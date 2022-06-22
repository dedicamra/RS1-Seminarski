using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Podaci.Klase;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Helper;
using WebApplication1.Models.VoziloAngular;

namespace WebApplication1.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [EnableCors]
    [Autorizacija(menadzer: true, kupac: false)]
    public class VoziloAngularController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<Korisnik> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VoziloAngularController(ApplicationDbContext Db, UserManager<Korisnik> userManager, IHttpContextAccessor httpContextAccessor)
        {
            db = Db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Prikaz(string pretraga)
        {
            List<VoziloPrikazVM.Row> Vozila = db.Vozilo
                .Where(v => pretraga == null || v.RegistracijskiBroj.ToLower().StartsWith(pretraga.ToLower()) || v.OznakaVozila.ToLower().StartsWith(pretraga.ToLower()))
                .Select(v => new VoziloPrikazVM.Row
                {
                    id = v.VoziloID,
                    oznakaVozila = v.OznakaVozila,
                    registracijskiBroj = v.RegistracijskiBroj,
                    maxBrojSjedista = v.MaxBrojSjedista,
                    datumZadnjegServisa = v.DatumZadnjegServisa.ToString()
                }).ToList();
            VoziloPrikazVM v = new VoziloPrikazVM();
            v.pretraga = pretraga;
            v.vozila = Vozila;


            return Ok(v);
        }

        [HttpDelete]//("{VoziloID}")
        public IActionResult Obrisi(int VoziloID)
        {
            Vozilo v = db.Vozilo.Find(VoziloID);
            db.Remove(v);
            db.SaveChanges();

            return Ok();
        }

        [HttpGet]//("{VoziloID}")
        public IActionResult ObrisiGET(int VoziloID)
        {
            Vozilo v = db.Vozilo.Find(VoziloID);
            db.Remove(v);
            db.SaveChanges();

            return Ok();
        }


        //privremeno rjesenje, dok asistent ne odgovori. nmg naci razlog zasto mi model bude null
        [HttpPost]
        //public IActionResult Snimi([FromBody] VoziloPrikazVM.Row x)
        public IActionResult Snimi(int id, string oznakaVozila, string registracijskiBroj, int maxBrojSjedista, string datumZadnjegServisa)
        {
            Vozilo vozilo;
            if (id == 0)
            {
                vozilo = new Vozilo();
                db.Add(vozilo);

            }
            else
            {
                vozilo = db.Vozilo.Find(id);
            }
            vozilo.OznakaVozila = oznakaVozila;
            vozilo.DatumZadnjegServisa = datumZadnjegServisa;
            vozilo.RegistracijskiBroj = registracijskiBroj;
            vozilo.MaxBrojSjedista = maxBrojSjedista;

            if (id != 0)
                db.Vozilo.Update(vozilo);

            db.SaveChanges();
            return Ok();
        }
    }
}
