using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Podaci.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Helper;
using WebApplication1.Models.Cijena;

namespace WebApplication1.Controllers
{
    [Autorizacija(menadzer:true,kupac:false)]
    public class CijenaController : Controller
    {
        private readonly ApplicationDbContext db;
        public CijenaController(ApplicationDbContext Db)
        {
            db = Db;
        }

        public IActionResult Prikaz(string pretraga)
        {
            List<CijenaPrikazVM.Row> cijene = db.Cijena
                .Where(c => pretraga == null || c.GradPolaska.Naziv.ToLower().StartsWith(pretraga.ToLower())
                                          || c.GradDolaska.Naziv.ToLower().StartsWith(pretraga.ToLower()))
                .Select(c => new CijenaPrikazVM.Row()
                {
                    CijenaID = c.CijenaID,
                    GradPolaskaGradID = c.GradPolaskaGradID,
                    GradPolaska = c.GradPolaska.Naziv,
                    GradDolaskaGradID=c.GradDolaskaGradID,
                    GradDolaska=c.GradDolaska.Naziv,
                    JednosmijernaKartaCijena=c.JednosmijernaKartaCijena,
                    PovratnaKartaCijena=c.PovratnaKartaCijena
                }).ToList();

            CijenaPrikazVM c = new CijenaPrikazVM();
            c.Cijene = cijene;
            c.pretraga = pretraga;

            return View(c);
        }

        public IActionResult Obrisi(int CijenaID)
        {
            Cijena cijena = db.Cijena.Find(CijenaID);
            db.Remove(cijena);
            db.SaveChanges();

            return Redirect("/Cijena/Prikaz");
        }

        public IActionResult Uredi(int CijenaID)
        {
            List<SelectListItem> gradovi = db.Grad.OrderBy(g => g.Naziv)
                .Select(g => new SelectListItem
                {
                    Text = g.Naziv,
                    Value = g.GradID.ToString()
                }).ToList();

                CijenaUrediVM cijena = CijenaID == 0 ? new CijenaUrediVM() :
                db.Cijena.Where(c => c.CijenaID == CijenaID)
                .Select(c => new CijenaUrediVM
                {
                    CijenaID = c.CijenaID,
                    GradDolaska = c.GradDolaska.Naziv,
                    GradDolaskaGradID = c.GradDolaskaGradID,
                    GradPolaska = c.GradPolaska.Naziv,
                    GradPolaskaGradID = c.GradPolaskaGradID,
                    JednosmijernaKartaCijena = c.JednosmijernaKartaCijena,
                    PovratnaKartaCijena = c.PovratnaKartaCijena
                }).Single();

            cijena.Gradovi = gradovi;

            return View("Uredi", cijena);
        }

        public IActionResult Snimi(CijenaUrediVM x)
        {
            Cijena cijena;
            if (x.CijenaID == 0)
            {
                cijena = new Cijena();
                db.Add(cijena);
            }
            else
            {
                cijena = db.Cijena.Find(x.CijenaID);
            }

            cijena.GradDolaskaGradID = x.GradDolaskaGradID;
            cijena.GradPolaskaGradID = x.GradPolaskaGradID;
            cijena.JednosmijernaKartaCijena = x.JednosmijernaKartaCijena;
            cijena.PovratnaKartaCijena = x.PovratnaKartaCijena;

            if(x.CijenaID != 0)
                db.Cijena.Update(cijena);

            db.SaveChanges();

            return Redirect("/Cijena/Prikaz");
        }

    }
}
