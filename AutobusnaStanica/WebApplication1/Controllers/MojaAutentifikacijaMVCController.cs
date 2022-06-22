using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Podaci.Klase;
using WebApplication1.Data;
using WebApplication1.Helper;
using WebApplication1.Models.Autentifikacija;

namespace WebApplication1.Controllers
{
    public class MojaAutentifikacijaMVCController : Controller
    {
        public static Korisnik logiraniKorisnik = null;
        private readonly ApplicationDbContext db;
        public MojaAutentifikacijaMVCController(ApplicationDbContext Db)
        {
            db = Db;
        }
        public IActionResult Login(string email="test@gmail.com",string password="Mostar2020!")
        {
            Korisnik nalog = db.Users.Where(s => s.Email == email).FirstOrDefault();
            if (nalog == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            HttpContext.SetLogiraniKorisnik(nalog);
            return Redirect("/");
        }
        public IActionResult Logout()
        {
            HttpContext.SetLogiraniKorisnik(null);
            return Redirect("/");
        }
       
    }
}