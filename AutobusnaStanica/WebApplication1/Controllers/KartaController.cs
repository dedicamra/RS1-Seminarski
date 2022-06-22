using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Podaci.Klase;
using WebApplication1.Data;
using WebApplication1.Helper;
using WebApplication1.Models.Karta;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Utilities;
using Twilio;
using Twilio.Rest.Api.V2010.Account;


namespace WebApplication1.Controllers
{
    [Autorizacija(menadzer:false,kupac:true)]
    public class KartaController : Controller
    {
        private readonly ApplicationDbContext db;
        public static List<KartaPrikazVM.Row> polazni = null;
        public static List<KartaPrikazVM.Row> dolazni = null;
        private readonly UserManager<Korisnik> userManager;
        private readonly SignInManager<Korisnik> signInManager;
        private readonly IEmailService _emailService;
        private readonly IClient _smsClientService;
        public KartaController(ApplicationDbContext Db, UserManager<Korisnik> um, SignInManager<Korisnik> sm, IEmailService emailService, IClient smsClientService)
        {
            db = Db;
            userManager = um;
            signInManager = sm;

            _emailService = emailService;
            _smsClientService = smsClientService;
        }
       
        public IActionResult Prikaz(KartaPrikazVM karte)
        {
            KartaPrikazVM m;
            List<SelectListItem> polazneLinije = new List<SelectListItem>();
            List<SelectListItem> dolazneLinije = new List<SelectListItem>();

            if (polazni == null || polazni.Count()==0)
            {
                m = new KartaPrikazVM();
                m.polazni = null;
                m.dolazni = null;
            }
            else
            {
                m = new KartaPrikazVM()
                {
                    TipKarteID = karte.TipKarteID,
                    DatumPolaska = karte.DatumPolaska,
                    DatumDolaska = karte.DatumDolaska,
                    DolazisteID = karte.DolazisteID,
                    PolazisteID = karte.PolazisteID,
                    OznakaLinije=polazni.First().ImeLinije+" - "+polazni.First().VrijemePolaska,//+"\n"+dolazni.First().ImeLinije,
                    polazni = polazni,
                    dolazni=dolazni,
                };
                foreach (var p in polazni)
                {
                    if (p.SlobodnihMjesta > 0)
                    {
                        var item = new SelectListItem
                        {
                            Text = p.ImeLinije + " - " + p.VrijemePolaska,
                            Value = p.LinijaId.ToString()
                        };
                        polazneLinije.Add(item);
                    }
                }
                foreach (var p in dolazni)
                {
                    if (p.SlobodnihMjesta > 0)
                    {
                        var item = new SelectListItem
                        {
                            Text = p.ImeLinije + " - " + p.VrijemePolaska,
                            Value = p.LinijaId.ToString()
                        };
                        dolazneLinije.Add(item);
                    }
                }
            }
           
            List<SelectListItem> tip = db.TipKarte.OrderBy(n => n.Naziv).Where(s=>s.IsAktivan==true)
                .Select(t => new SelectListItem
            {
                Text = t.Naziv,
                Value = t.TipKarteID.ToString()
            }).ToList();

            List<SelectListItem> pol = db.Grad.Where(x=>x.IsDeleted==false).OrderBy(n => n.Naziv).Select(t => new SelectListItem
            {
                Text = t.Naziv,
                Value = t.GradID.ToString()
            }).ToList();
            List<SelectListItem> dol = db.Grad.Where(x => x.IsDeleted == false).OrderBy(n => n.Naziv).Select(t => new SelectListItem
            {
                Text = t.Naziv,
                Value = t.GradID.ToString()
            }).ToList();

            m.LinijaDolaziste = dolazneLinije;
            m.LinijaPolaziste = polazneLinije;
            m.TipKarte = tip;
            m.Polaziste = pol;
            m.Dolaziste = dol;
            m.popusti = ListaPopustaHelper();
            m.kolicina = new int[m.popusti.Count()];
            return View(m);
        }
        public IActionResult Uredi(KartaPrikazVM x)
        {
            polazni = PronadjiLinije(x);
            int temp = x.PolazisteID;
            x.PolazisteID = x.DolazisteID;
            x.DolazisteID = temp;
            dolazni = PronadjiLinije(x);


            KartaPrikazVM xz = new KartaPrikazVM()
            {
                DolazisteID = x.PolazisteID,
                PolazisteID = x.DolazisteID,
                TipKarteID = x.TipKarteID,
                DatumPolaska = x.DatumPolaska,
                DatumDolaska = x.DatumDolaska,
                OznakaLinije=x.OznakaLinije
            };
           
            return RedirectToAction("Prikaz",xz);
           
        }
        public List<KartaPrikazVM.Row> PronadjiLinije(KartaPrikazVM x)
        {
            int polazisteRedniBroj = -1, dolazisteRedniBroj = -1;
            string vrijemePolaska = "", vrijemeDolaska = "", gradP = "", gradD = "";
            int linijaId = 0;

            List<KartaPrikazVM.Row> podlinije = new List<KartaPrikazVM.Row>();
            foreach (var t in db.Stajalista)
            {
                if (t.GradID == x.PolazisteID)
                {
                    polazisteRedniBroj = t.RedniBrojStajalista;
                    vrijemePolaska = t.SatnicaStizanja;
                    linijaId = t.LinijaID;
                    gradP = db.Grad.Find(t.GradID).Naziv;
                }
                if (t.GradID == x.DolazisteID && linijaId == t.LinijaID)
                {
                    dolazisteRedniBroj = t.RedniBrojStajalista;
                    vrijemeDolaska = t.SatnicaStizanja;
                    gradD = db.Grad.Find(t.GradID).Naziv;
                }
                if (polazisteRedniBroj < dolazisteRedniBroj && polazisteRedniBroj != -1 && dolazisteRedniBroj != -1)
                {
                    var linija = db.Linija.Where(l => l.LinijaID == linijaId).SingleOrDefault();
                    var parsedDate = DateTime.Parse(x.DatumPolaska);
                    var parsedTime = DateTime.Parse(vrijemePolaska);
                    if (linija.DaniUSedmici.Contains(parsedDate.DayOfWeek.ToString()))
                    {
                        if((parsedDate.Date==DateTime.Now.Date && parsedTime.TimeOfDay > DateTime.Now.TimeOfDay)
                         || parsedDate.Date != DateTime.Now.Date)
                        {
                            var nL = db.Linija.Where(d => d.LinijaID == linijaId).FirstOrDefault();
                            var brojZM = db.Karta.Where(w => w.NazivLinije.Contains(nL.OznakaLinije) && w.DatumPolaska==x.DatumPolaska).Count();
                            podlinije.Add(new KartaPrikazVM.Row()
                            {
                                LinijaId = linijaId,
                                RedniBrojPolazista = polazisteRedniBroj,
                                RedniBrojDolazista = dolazisteRedniBroj,
                                VrijemePolaska = vrijemePolaska,
                                VrijemeDolaska = vrijemeDolaska,
                                PolazisteNaziv = gradP,
                                DolazisteNaziv = gradD,
                                SlobodnihMjesta = 50 - brojZM
                            });
                        }
                    }
                    polazisteRedniBroj = -1;
                    dolazisteRedniBroj = -1;
                }
            }
            string stanica = "";

            List<KartaPrikazVM.Row> Pkarte = new List<KartaPrikazVM.Row>();
            foreach (var t in podlinije)
            {
                foreach (var i in db.Stajalista)
                {
                    if (t.LinijaId == i.LinijaID)
                    {
                        if (i.RedniBrojStajalista > t.RedniBrojPolazista && i.RedniBrojStajalista < t.RedniBrojDolazista)
                            stanica += db.Grad.Find(i.GradID).Naziv + " " + i.SatnicaStizanja + "\n";
                    }
                }
               

                KartaPrikazVM.Row y = new KartaPrikazVM.Row()
                {
                    LinijaId = t.LinijaId,
                    PolazisteNaziv = t.PolazisteNaziv,
                    DolazisteNaziv = t.DolazisteNaziv,
                    ImeLinije = db.Linija.Find(t.LinijaId).OznakaLinije,
                    VrijemePolaska = t.VrijemePolaska,
                    VrijemeDolaska = t.VrijemeDolaska,
                    Stajalista = stanica == "" ? "nema stajanja" : stanica,
                    SlobodnihMjesta=t.SlobodnihMjesta
                };
                Pkarte.Add(y);
                stanica = "";
            }
            return Pkarte;
        }
        [HttpPost]
        public IActionResult Kupovina(KartaPrikazVM x)
        {
            bool provjera = false;
            for (int i = 0; i < db.VrstaPopusta.Where(s=>s.IsAktivan==true).Count(); i++)
            {
                if (x.kolicina[i] > 0)
                    provjera = true;
            }
            if (!provjera || (x.TipKarteID==2 && x.LinijaDolazisteID==0))
                return Redirect(Request.Headers["Referer"].ToString());

            var cijena = db.Cijena.Where(c => (c.GradPolaskaGradID == x.PolazisteID && c.GradDolaskaGradID == x.DolazisteID)
              || (c.GradDolaskaGradID == x.PolazisteID && c.GradPolaskaGradID == x.DolazisteID)).SingleOrDefault();

            if(cijena==null)
                return Redirect(Request.Headers["Referer"].ToString());

            float iznos = 0;
            if (x.TipKarteID == 1)
                iznos = cijena.JednosmijernaKartaCijena;
            else
                iznos = cijena.PovratnaKartaCijena;

            float ukupna=0;
            var br = 0;
           
            foreach (var iz in db.VrstaPopusta.Where(s=>s.IsAktivan))
            {
                if(x.kolicina[br]>0)
                    ukupna += (iznos - iznos * iz.Iznos) * x.kolicina[br];
                br++;
            }

            var listak = db.KreditnaKartica.Where(k => k.Kupac.Id == HttpContext.GetLogiranogUsera().Id && k.IsAktivna).Select(k => new SelectListItem
            {
                Value = k.KreditnaKarticaID.ToString(),
                Text = k.BrojKartice.Substring(0, 6) + "xx xxxx" + k.BrojKartice.Substring(14, 5),
            }).ToList();
            var linijaOD = db.Linija.Where(l => l.LinijaID == x.LinijaDolazisteID).Select(l => l.OznakaLinije + " - " + l.VrijemePolaska).SingleOrDefault();
            var linijaOP = db.Linija.Where(l => l.LinijaID == x.LinijaPolazisteID).Select(l => l.OznakaLinije + " - " + l.VrijemePolaska).SingleOrDefault();
            var m = new KartaKupovinaVM
            {
                CijenaBezPopusta = iznos,
                Cijena = (float)Math.Round(ukupna, 2),
                PolazisteID = x.PolazisteID,
                DolazisteID = x.DolazisteID,
                DatumDolaska = x.DatumDolaska,
                DatumPolaska = x.DatumPolaska,
                TipKarteID = x.TipKarteID,
                LinijaDolaziste = linijaOD,
                LinijaPolaziste = linijaOP,
                DolazisteNaziv = db.Grad.Where(g => g.GradID == x.DolazisteID).Select(g => g.Naziv).SingleOrDefault(),
                PolazisteNaziv = db.Grad.Where(g => g.GradID == x.PolazisteID).Select(g => g.Naziv).SingleOrDefault(),
                TipKarte = db.TipKarte.Where(t => t.TipKarteID == x.TipKarteID).Select(t => t.Naziv).SingleOrDefault(),
                Kartice = listak,
                OznakaLinije = linijaOP+linijaOD,
                popusti = ListaPopustaHelper(),
                kolicina=x.kolicina
            };
          
            return View(m);
        }
     
        public async Task<IActionResult> PlacanjeAsync(KartaKupovinaVM x)
        {
            KreditnaKartica kk=null;
            if (x.SpasiKarticu)
            {
                kk = new KreditnaKartica();
                kk.DatumIsteka = x.DatumIstekaNova;
                kk.BrojKartice = x.BrojKarticeNova;
                kk.ImeVlasnikaKartice = x.ImeVlasnikaNova;
                kk.VerifikacijskiKod = x.VerKodNova;
                kk.IsAktivna = true;
                kk.Kupac =  (Kupac)HttpContext.GetLogiranogUsera();
                db.Entry<Kupac>(kk.Kupac).State= EntityState.Unchanged;

                db.Attach(kk);
                db.Entry(kk).State = EntityState.Added;
                db.KreditnaKartica.Add(kk);
                //db.Entry<Kupac>(kk.Kupac).State= EntityState.Detached;
                db.SaveChanges();
            }
            int[] nizVrsta = new int[db.VrstaPopusta.Where(s=>s.IsAktivan).Count()];
            int brojacNiza = 0;
            foreach (var item in db.VrstaPopusta)
            {
                if (item.IsAktivan)
                {
                    nizVrsta[brojacNiza++] = item.VrstaPopustaID;
                }
            }
            //IQueryable<Object> objects = db.VrstaPopusta;
            int ind = 0;
            brojacNiza = 0;
          
            for (int i = 0; i < db.VrstaPopusta.Where(s => s.IsAktivan).Count(); i++)
            {
                if (x.kolicina[ind] > 0)
                {
                    for (int j = 0; j < x.kolicina[ind]; j++)
                    {
                        Karta k = new Karta()
                        {
                            DatumKupovine = DateTime.Now.Date.ToString("yyyy-MM-dd"),
                            DatumPolaska = x.DatumPolaska,
                            DatumDolaska = x.TipKarteID == 2 ? x.DatumDolaska : null,
                            TipKarteID = x.TipKarteID,
                            VrstaPopustaID = nizVrsta[brojacNiza],
                            NazivLinije = x.LinijaPolaziste+Environment.NewLine+x.LinijaDolaziste,
                            IsAktivna = true,
                            Cijena = (float)Math.Round(x.CijenaBezPopusta - x.CijenaBezPopusta * db.VrstaPopusta.Find(nizVrsta[brojacNiza]).Iznos, 2),
                            Kupac =null,
                            Kupac_ID= HttpContext.GetLogiranogUsera().Id
                        };
                        if (kk != null)
                            k.KKarticaID = kk.KreditnaKarticaID;
                        else if (x.BrojKarticeNova == null)
                            k.KKarticaID = x.KarticeID;
                        else
                            k.KKarticaID = null;

                        k.PolazisteID = x.PolazisteID;
                        k.DolazisteID = x.DolazisteID;

                        db.Karta.Add(k);
                        db.SaveChanges();
                        
                    }
                }
                brojacNiza++;
                ind++;
            }

     
            polazni = null;
            dolazni = null;


            //posalji mail s kartom 
            #region Mail //Amra
            var popusti = db.VrstaPopusta.Where(x => x.IsAktivan == true).ToList();
            var tipKarte = db.TipKarte.Where(a => a.TipKarteID == x.TipKarteID).FirstOrDefault().Naziv;
            var polaziste = db.Grad.Where(a => a.GradID == x.PolazisteID).FirstOrDefault()?.Naziv;
            var dolaziste = db.Grad.Where(a => a.GradID == x.DolazisteID).FirstOrDefault()?.Naziv;
            var kupac = (Kupac)HttpContext.GetLogiranogUsera();
            string message = $@"<p>Zdravo {kupac.Ime},</p></br>
                                <p>Zahvaljujemo što ste izabrali nas za Vaše potrebe prevoza.</p>
                                <p>U nastavku se nalaze informacije o kupljenoj karti koju možete iskoristiti prilikom ulaska u autobus.</p></br>
                                <h4>Detalji o karti</h4>
                                <p>Tip karte: {tipKarte}</p>
                                <p>Linija: {x.LinijaPolaziste}</p>
                                <p>Na relaciji: {polaziste} - {dolaziste}</p>
                                <p>Datum polaska: {x.DatumPolaska} </p>";
            if (x.TipKarteID == 2)
            {
                message += $@" <p>Povratna linija: {x.LinijaDolaziste} </p>
                                <p>Datum povratka: {x.DatumDolaska} </p>";
            }
            int brojac = 0;

            message += $@"<p>Količina karata: </p>";
            foreach (var item in popusti)
            {
                if (x.kolicina[brojac] != 0)
                {
                    message += $@"
                                <p>{item.Naziv}: {x.kolicina[brojac]}x</p>";
                }
                brojac++;
            }
            message += $@"<p>Ukupna cijena: {x.Cijena} KM</p></br></br></br>
                          <p>Želimo Vam ugodno putovanje!</p>";

             await _emailService.SendEmailAsync(
                email: kupac.Email,
                subject: "Kupovina karte",
                body: $@"{message}"
                );

            #endregion
            #region SMS
            if (kupac.PhoneNumber != null)
            {
                var toPhoneNumber = $"{kupac.PhoneNumber}";
                var sms = $@"Kupovina karte uspješno izvršena. Za više informacija provjerite Vašu email adresu. Hvala na povjerenju!";

                await _smsClientService.SendSmsAsync(toPhoneNumber, sms);

            }
            #endregion

            return RedirectToAction("KupljeneKarte");
        }
        public IActionResult KupljeneKarte()
        {
            polazni = null;
            dolazni = null;
            foreach(var i in db.Karta)
            {
                if (i.TipKarteID == 1 && DateTime.Compare(DateTime.Parse(i.DatumPolaska), DateTime.Now) == -1)
                {
                    if (i.TipKarteID == 2 && DateTime.Compare(DateTime.Parse(i.DatumDolaska), DateTime.Now) == -1)
                        i.IsAktivna = false;
                    db.Attach(i);
                    db.Entry(i).State = EntityState.Modified;
                    i.IsAktivna = false;
                }
                else
                    i.IsAktivna = true;
            }
            db.SaveChanges();
            var redovi = db.Karta
                .Include(i => i.Polaziste.Grad)
                .Include(i => i.Dolaziste.Grad)
                .Include(i => i.TipKarte)
                .Include(i => i.VrstaPopusta)
                .AsEnumerable()
                .OrderByDescending(i=>i.KartaID)
                .Where(i => i.Kupac_ID == HttpContext.GetLogiranogUsera().Id).Select(i => new KupljeneKarteVM.Row
            {
                Cijena = i.Cijena,
                DatumPolaska = i.DatumPolaska,
                DatumDolaska = i.DatumDolaska,
                PolazisteNaziv = db.Grad.Where(p=>p.GradID==i.PolazisteID).FirstOrDefault().Naziv,
                DolazisteNaziv = db.Grad.Where(p => p.GradID == i.DolazisteID).FirstOrDefault().Naziv,
                IsAktivna = i.IsAktivna,
                KartaID = i.KartaID,
                OznakaLinije = i.NazivLinije,
                TipKarte = i.TipKarte.Naziv,
                VrstaPopusta = i.VrstaPopusta.Naziv,
                DatumKupovineKarte=i.DatumKupovine
            }).ToList();
         
            var m = new KupljeneKarteVM
            {
                redovi = redovi
            };
            return View(m);
        }
        public IActionResult OtkaziKartu(int KartaID)
        {
            var karta = db.Karta.Find(KartaID);
            db.Remove(karta);
            db.SaveChanges();
            return Redirect("KupljeneKarte");
        }
        public List<string> ListaPopustaHelper()
        {
            List<string> vrsta = new List<string>();
            foreach (var item in db.VrstaPopusta)
            {
                if (item.IsAktivan)
                {
                    vrsta.Add(item.Naziv);
                }
            }
            return vrsta;
        }



       
    }
}