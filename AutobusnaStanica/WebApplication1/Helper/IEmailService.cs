using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);

    }
}
