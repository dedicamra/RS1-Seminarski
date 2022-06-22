 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Podaci.Klase;
using WebApplication1.Data;
using WebApplication1.Helper;
using WebApplication1.Models.Karta;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    [Autorizacija(menadzer: false, kupac: true)]
    public class KreditnaKarticaController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<Korisnik> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public KreditnaKarticaController(ApplicationDbContext Db, UserManager<Korisnik> userManager,IHttpContextAccessor httpContextAccessor)
        {
            db = Db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<KreditnaKarticaPrikazVM> KreditnaKarticaPrikaz()
        {
            var ss=HttpContext.User.Identity.Name;
            var kartice = new List<KreditnaKarticaPrikazVM.KarticaRedovi>();
            kartice = await db.KreditnaKartica.Include(d=>d.Kupac).Where(k => k.Kupac.Id == AutentifikacijaMVC.currentUserId && k.IsAktivna).Select(k => new KreditnaKarticaPrikazVM.KarticaRedovi
            {
                kreditnaKarticaID = k.KreditnaKarticaID,
                brojKartice =  k.BrojKartice,//.Substring(0,6)+"xx xxxx"+k.BrojKartice.Substring(14,5),
                datumIsteka = k.DatumIsteka,
                imeVlasnika = k.ImeVlasnikaKartice,
                verKod = k.VerifikacijskiKod
            }).ToListAsync();
          
            var m = new KreditnaKarticaPrikazVM
            {
                karticeKupca = kartice
            };
            return m; 
        }
        [HttpDelete("{KarticaID}")]
        public IActionResult KreditnaKarticaObrisi(int KarticaID)
        {
            KreditnaKartica v = db.KreditnaKartica.Find(KarticaID);
            var listaK = db.Karta.Where(s => s.KKarticaID == KarticaID).ToList();

            foreach (var k in listaK)
            {
                k.KKarticaID = null;
                db.Attach(k);
                db.Entry(k).State = EntityState.Modified;
            }
            //db.RemoveRange(listaK);
            db.SaveChanges();
            db.Remove(v);
            db.Attach(v);
            db.Entry(v).State = EntityState.Deleted;
            //v.IsAktivna = false;
            db.SaveChanges();

            return Ok();
        }
        
        [HttpPut]
        public IActionResult KreditnaKarticaSnimi([FromBody] KreditnaKarticaPrikazVM.KarticaRedovi x)
        {
            KreditnaKartica kartica =db.KreditnaKartica.Find(x.kreditnaKarticaID);
            kartica.ImeVlasnikaKartice = x.imeVlasnika;
            kartica.VerifikacijskiKod = x.verKod;
            kartica.DatumIsteka = x.datumIsteka;
            kartica.BrojKartice = x.brojKartice;
            kartica.IsAktivna = true;
            db.Attach(kartica);
            db.Entry(kartica).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }
        [HttpPost]
        public IActionResult KreditnaKarticaDodaj([FromBody] KreditnaKarticaPrikazVM.KarticaRedovi x)
        {
            
            KreditnaKartica kartica = new KreditnaKartica()
            {
                Kupac=(Kupac)db.Users.FirstOrDefault(s=>s.Id==AutentifikacijaMVC.currentUserId),// (Kupac)HttpContext.GetLogiranogUsera(),//ovdje je belaj
                ImeVlasnikaKartice = x.imeVlasnika,
                VerifikacijskiKod = x.verKod,
                DatumIsteka = x.datumIsteka,
                BrojKartice = x.brojKartice,
                IsAktivna=true
            };
            db.Attach(kartica);
            db.Entry(kartica).State = EntityState.Added;
            db.KreditnaKartica.Add(kartica);
            db.SaveChanges();

            return Ok();
        }
    }
}