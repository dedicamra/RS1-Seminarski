using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Podaci.Klase;
using WebApplication1.Data;
using WebApplication1.Helper;
using WebApplication1.Models;
using WebApplication1.Models.Menadzer;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;
        private readonly UserManager<Korisnik> userManager;
        private readonly SignInManager<Korisnik> signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<Korisnik> um, SignInManager<Korisnik> sm,ApplicationDbContext DB)
        {
            _logger = logger;
            userManager = um;
            signInManager = sm;
            db = DB;
        }
        [Autorizacija(menadzer:true,kupac:true)]
        public IActionResult Index()
        {
            KartaController.polazni = null;
            KartaController.dolazni = null;
            List<ObavijestPrikazVM.Row> obavijesti = db.Obavijest.OrderByDescending(s => s.ObavijestID)
               .Select(o => new ObavijestPrikazVM.Row
               {
                   ObavijestID = o.ObavijestID,
                   Naslov = o.Naslov,
                   Podnaslov = o.Podnaslov,
                   Opis = o.Opis.Substring(0, 100)+"...",
                   DatumObjave = o.DatumObjave,
                   ObavijestKategorija = o.ObavijestKategorija.Naziv,
                   Slika = KonvertovanjeSlike.GetImageBase64(o.Slika)
               }).ToList();
            ObavijestPrikazVM m = new ObavijestPrikazVM();
            m.obavijesti = obavijesti;
            return View(m); 
        }
        [Autorizacija(menadzer: true, kupac: true)]
        public IActionResult ObavijestDetalji(int ObavijestID)
        {
            var obavijest = db.Obavijest.Where(i => i.ObavijestID == ObavijestID).Select(o=>new ObavijestPrikazVM.Row
            {
                Naslov = o.Naslov,
                Podnaslov = o.Podnaslov,
                Opis = o.Opis,
                DatumObjave = o.DatumObjave,
                ObavijestKategorija = o.ObavijestKategorija.Naziv,
                Slika = KonvertovanjeSlike.GetImageBase64(o.Slika)
            }).FirstOrDefault();
           
            return View(obavijest);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Autorizacija(menadzer:true,kupac:false)]
        public IActionResult DodajMenadzera()
        {
            return View();
        }
        public IActionResult Snimi(string ime, string prezime)
        {
            string email = ime.ToLower() + "." + prezime.ToLower() + "@edu.fit.ba"; //dodala sam ti ToLower da se ne spasava s mail s velikim slovima
            if (db.Users.Where(i => i.Email == email).SingleOrDefault()!=null)
            {
                email= ime + "1." + prezime + "@edu.fit.ba";
            }
            var korisnik = new Menadzer
            {
                Email = email,
                UserName = email,
                Ime = ime,
                Prezime = prezime,
                EmailConfirmed = true,
            };
            IdentityResult result = userManager.CreateAsync(korisnik, "Mostar2020!").Result;

            if (!result.Succeeded)
            {
                 Console.WriteLine("errors: " + string.Join('|', result.Errors));
                return DodajMenadzera();
            }
            Console.WriteLine("Menadzer je uspješno dodat");
            return RedirectToAction("Index");
        }
    }
}
