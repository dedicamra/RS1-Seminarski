using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempAppForReportDesign;
using WebApplication1.Data;
using WebApplication1.Helper;
using WebApplication1.Models.Menadzer;

namespace WebApplication1.Controllers
{
    public class IzvjestajController : Controller
    {
        private readonly ApplicationDbContext db;
        public IzvjestajController(ApplicationDbContext DB)
        {
            db = DB;
        }

        [Autorizacija(menadzer: true, kupac: false)]
        public IActionResult IzvjestajPrikaz(IzvjestajRequest i)
        {
            if (i.DatumOd > i.DatumDo)
                return Redirect("/Menadzer/Izvjestaj?ispravan=false");

            LocalReport _localReport = new LocalReport("Reports/Report1.rdlc");
            var podaci = getKarte(db, i.DatumOd, i.DatumDo);
            _localReport.AddDataSource("DataSet1", podaci);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ReportSastavio", db.Users.FirstOrDefault(s => s.Id == AutentifikacijaMVC.currentUserId).ToString());
            parameters.Add("dtmOd", i.DatumOd.ToShortDateString());
            parameters.Add("dtmDo", i.DatumDo.ToShortDateString());

            ReportResult result = _localReport.Execute(RenderType.Pdf, parameters: parameters);

            return File(result.MainStream, "application/pdf");
        }

        [Autorizacija(menadzer: true, kupac: false)]
        public static List<KartaRow> getKarte(ApplicationDbContext db, DateTime dtmOd, DateTime dtmDo)
        {

            List<KartaRow> podaci = db.Karta.Include(x => x.Polaziste.Grad)
                .Include(x => x.Dolaziste.Grad)
                .Include(x => x.VrstaPopusta)
                .Include(x => x.TipKarte).ToList()
                .Where(x => DateTime.Parse(x.DatumKupovine) >= dtmOd && DateTime.Parse(x.DatumKupovine) <= dtmDo)
                .Select(x => new KartaRow
                {
                    PolazisteNaziv = x.Polaziste.Grad.Naziv,
                    DolazisteNaziv = x.Dolaziste.Grad.Naziv,
                    TipKarte = x.TipKarte.Naziv,
                    OznakaLinije = x.NazivLinije,
                    DatumKupovineKarte = x.DatumKupovine,
                    DatumDolaska = x.DatumDolaska,
                    DatumPolaska = x.DatumPolaska,
                    VrstaPopusta = x.VrstaPopusta.Naziv,
                    Cijena = x.Cijena
                }).OrderBy(x => x.DatumKupovineKarte).ToList();

            return podaci;
        }


        [Autorizacija(menadzer: true, kupac: true)]
        public IActionResult RedVoznje()
        {
            LocalReport _localReport = new LocalReport("Reports/RedVoznje.rdlc");
            var podaci = getLinije(db);
            _localReport.AddDataSource("LinijaRow", podaci);

            ReportResult result = _localReport.Execute(RenderType.Pdf);

            return File(result.MainStream, "application/pdf");
        }
        public static List<LinijaRow> getLinije(ApplicationDbContext db)
        {

            var linije = db.Linija.Where(x=>x.IsDeleted==false).Select(x => new LinijaRow
            {
                Linija = x.OznakaLinije,
                LinijaId = x.LinijaID,
                DaniUSedmiciArray = x.DaniUSedmici
            }).ToList();

            foreach (var l in linije)
            {
                foreach (var d in l.DaniUSedmiciArray)
                {
                    if (d == "Monday")
                        l.DaniUSedmici += "Ponedjeljak. ";
                    else if (d == "Tuesday")
                        l.DaniUSedmici += "Utorak. ";
                    else if (d == "Wednesday")
                        l.DaniUSedmici += "Srijeda. ";
                    else if (d == "Thursday")
                        l.DaniUSedmici += "Četvrtak. ";
                    else if (d == "Friday")
                        l.DaniUSedmici += "Petak. ";
                    else if (d == "Saturday")
                        l.DaniUSedmici += "Subota. ";
                    else
                        l.DaniUSedmici += "Nedjelja. ";

                }
                var stajalista = db.Stajalista.Where(x => x.LinijaID == l.LinijaId).Include(x => x.Grad).OrderBy(x => x.RedniBrojStajalista).ToList();

                foreach (var s in stajalista)
                {
                    l.StajalistaSatnice += s.Grad.Naziv + "(" + s.SatnicaStizanja + ") ";
                }
            }

            return linije;
        }

    }
}
