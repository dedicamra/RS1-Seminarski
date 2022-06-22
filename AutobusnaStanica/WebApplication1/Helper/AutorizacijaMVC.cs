using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Podaci.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;


namespace WebApplication1.Helper
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool menadzer, bool kupac)
            : base(typeof(AutorizacijaMVC))
        {
            Arguments = new object[] { menadzer, kupac};
        }
    }
    public class AutorizacijaMVC : IAsyncActionFilter
    {
        public AutorizacijaMVC(bool menadzer, bool kupac)
        {
            _menadzer = menadzer;
            _kupac = kupac;
        }
        private readonly bool _menadzer;
        private readonly bool _kupac;
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Korisnik k = context.HttpContext.GetLogiranogUsera();
            ApplicationDbContext db = context.HttpContext.RequestServices.GetService<ApplicationDbContext>();
            if (k == null )
            {   
                if (context.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste logirani";
                }

                context.Result = new RedirectResult("/Identity/Account/Login");
                    return;
            }
            
            if (_menadzer && db.Menadzer.Any(s => s.Id == k.Id))
            {
                await next(); //ok - ima pravo pristupa
                return;
            }

            if (_kupac && db.Kupac.Any(s => s.Id == k.Id))
            {
                await next(); //ok - ima pravo pristupa
                return;
            }

            if (context.Controller is Controller c1)
            {
                c1.ViewData["error_poruka"] = "Nemate pravo pristupa";
            }
            context.Result = new RedirectToActionResult("Index", "Home", new { @area = "" });

            return;
        }
    }
}
