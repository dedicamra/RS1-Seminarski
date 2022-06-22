using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Podaci.Klase;
using System;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Helper;
using WebApplication1.Models.Feedback;

namespace WebApplication1.Controllers
{
    [Autorizacija(menadzer: false, kupac: true)]
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<Korisnik> userManager;
        private readonly SignInManager<Korisnik> signInManager;

        public FeedbackController(ApplicationDbContext db, UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Prikaz()
        {

            return View("Prikaz");
        }
        public IActionResult GetFeedbacks(int moje = 0)
        {
            var m = new FeedbackPrikazVM();
            m.moje = moje;
            if (moje == 1)
            {
                var korisnikId = db.Users.FirstOrDefault(k => k.Id == AutentifikacijaMVC.currentUserId).Id;
                m.feedbacks = db.Feedback.Include(x => x.Kupac).Where(x => x.Kupac.Id == korisnikId && x.IsDeleted == false).Select(x => new FeedbackPrikazVM.Feedback
                {
                    Id = x.Id,
                    DatumKreiranja = x.DatumKreiranja.ToShortDateString(),
                    KupacIme = db.Users.FirstOrDefault(k => k.Id == x.Kupac.Id).Ime,
                    KupacPrezime = db.Users.FirstOrDefault(k => k.Id == x.Kupac.Id).Prezime,
                    Sadrzaj = x.Sadrzaj,
                    IsDeleted = x.IsDeleted,
                    Datum = x.DatumKreiranja
                }).OrderByDescending(x => x.Datum).ToList();
            }
            else
            {

                m.feedbacks = db.Feedback.Include(x => x.Kupac).Where(x => x.IsDeleted == false).Select(x => new FeedbackPrikazVM.Feedback
                {
                    Id = x.Id,
                    DatumKreiranja = x.DatumKreiranja.ToShortDateString(),
                    KupacIme = db.Users.FirstOrDefault(k => k.Id == x.Kupac.Id).Ime,
                    KupacPrezime = db.Users.FirstOrDefault(k => k.Id == x.Kupac.Id).Prezime,
                    Sadrzaj = x.Sadrzaj,
                    IsDeleted = x.IsDeleted,
                    Datum = x.DatumKreiranja
                }).OrderByDescending(x => x.Datum).ToList();
            }

            return PartialView("FeedbackList", m);
        }

        public IActionResult DodajFeedback(int id = 0)
        {
            var m = new FeedbackDodajVM()
            {
                Id = id == 0 ? 0 : id,
                KorisnikImePrezime = HttpContext.GetLogiranogUsera().Ime + " " + HttpContext.GetLogiranogUsera().Prezime,
                Sadrzaj = id == 0 ? "" : db.Feedback.Find(id).Sadrzaj
            };

            return PartialView("FeedbackDodaj", m);
        }
        public IActionResult FeedbackSnimi(FeedbackDodajVM m)
        {
            var novi = new Feedback();
            if (m.Id == 0)
            {
                novi = new Feedback
                {
                    Kupac = (Kupac)db.Users.FirstOrDefault(k => k.Id == AutentifikacijaMVC.currentUserId),
                    Sadrzaj = m.Sadrzaj,
                    DatumKreiranja = DateTime.Now
                };
                db.Attach(novi);
                db.Entry(novi).State = EntityState.Added;
                db.Feedback.Add(novi);
            }
            else
            {
                novi = db.Feedback.Find(m.Id);
                novi.Sadrzaj = m.Sadrzaj;

                db.Feedback.Update(novi);
            }
            db.SaveChanges();

            return RedirectToAction("Prikaz");
        }

        public IActionResult ObrisiFeedback(int id)
        {
            var f = db.Feedback.Find(id);
            f.IsDeleted = true;
            db.Feedback.Update(f);
            db.SaveChanges();

            return RedirectToAction("Prikaz");
        }

    }
}
