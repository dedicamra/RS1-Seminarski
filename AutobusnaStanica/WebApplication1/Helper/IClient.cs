using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace WebApplication1.Helper
{
    public interface IClient
    {
        bool IsInitialized { get; set; }
        bool CanSendSms { get; }
        bool FromNumberRequired { get; }
       
        Task<IResponse> SendSmsAsync(string to, string msg);
        string ToString();
    }
}
