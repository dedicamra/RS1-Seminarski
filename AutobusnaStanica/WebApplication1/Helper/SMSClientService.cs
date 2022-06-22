using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Twilio;
using Twilio.Exceptions;
using Twilio.Http;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using WebApplication1.Utilities;

namespace WebApplication1.Helper
{
    public class SMSClientService: IClient
    {
        public bool CanSendSms { get { return true; } }
        public bool FromNumberRequired { get { return true; } }
        public bool IsInitialized { get; set; }

        public SMSClientService()
        {
            TwilioClient.Init(TwilioCredentials.TWILIO_ACC_SID, TwilioCredentials.TWILIO_AUTH_TOKEN);

            IsInitialized = true;
        }


        public async Task<IResponse> SendSmsAsync(string to, string msg)
        {
            var pnFrom = new PhoneNumber(TwilioCredentials.TWILIO_TRIAL_NUMBER);
            var pnTo = new PhoneNumber(to);


            var message = await MessageResource.CreateAsync(
              pnTo,
              from: pnFrom,
              body: msg
             );
            return new TextResponse(message);
        }

        public override string ToString()
        {
            return "Twilio API";
        }


        public class TextResponse : IResponse
        {
            private string m_sid;
            public bool CanUpdate { get { return true; } }
            public string Status { get; set; }


            public TextResponse(MessageResource message)
            {
                SetMessage(message);
            }


            private void SetMessage(MessageResource message)
            {
                m_sid = message.Sid;
                Status = message.Status.ToString();
            }



            public async Task UpdateAsync()
            {
                var message = await MessageResource.FetchAsync(m_sid);
                SetMessage(message);
            }
        }
    }
}
