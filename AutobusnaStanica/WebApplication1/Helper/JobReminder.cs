using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Helper
{
    public class JobReminder : IJob
    {
       // private readonly ApplicationDbContext _db;

        private readonly IEmailService _emailService;

        public JobReminder(IEmailService emailService)//, ApplicationDbContext db)
        {
            _emailService = emailService;
           // _db = db;
        }

        public Task Execute(IJobExecutionContext context)
        {
       
            var body = $@"<p>Zdravo kolege,</p></br>
                                <p>Ugodan radni dan Vam zelimo!</p><br><br>
                                <p>Lijep pozdrav</p>";

            //koristila bih ove mailove kad bi svi bili validni
            //var list = _db.Menadzer.Where(x => x.EmailConfirmed == true).Select(x => x.Email).ToList(); 
            
            //radi testiranja slat cu na dva moja maila
            var listaMailova = new List<string>();
            listaMailova.Add("merchantapp2@gmail.com");
            listaMailova.Add("dedic12315@gmail.com");
            foreach (var mail in listaMailova)
            {
                _emailService.SendEmailAsync(
                   email: mail,
                   subject: "Dobro jutro",
                   body: $@"{body}"
                   );
            }
            return Task.CompletedTask;
            
        }
    }
}
