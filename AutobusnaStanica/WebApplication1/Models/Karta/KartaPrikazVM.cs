using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Karta
{
    public class KartaPrikazVM
    {
        public class Row
        {
            public string VrijemePolaska { get; set; }
            public string VrijemeDolaska { get; set; }
            public string Satnica { get; set; }
            public string Stajalista { get; set; }
            public string PolazisteNaziv { get; set; }
            public string DolazisteNaziv { get; set; }
            public string ImeLinije { get; set; }
            public int SlobodnihMjesta { get; set; }
            public int LinijaId { get; set; }
            public int RedniBrojPolazista { get; set; }
            public int RedniBrojDolazista { get; set; }
        }
        public string DatumPolaska { get; set; }
        public string DatumDolaska { get; set; }
        public List<SelectListItem> TipKarte { get; set; }
        public int TipKarteID { get; set; }
        public List<SelectListItem> Polaziste { get; set; }
        public int PolazisteID { get; set; }
        public List<SelectListItem> Dolaziste { get; set; }
        public int DolazisteID { get; set; }
      
        public List<Row> polazni { get; set; }
        public List<Row> dolazni { get; set; }
        //linija
        public int LinijaPolazisteID { get; set; }
        public List<SelectListItem> LinijaPolaziste { get; set; }
        public int LinijaDolazisteID { get; set; }
        public List<SelectListItem> LinijaDolaziste { get; set; }
        public string OznakaLinije { get; set; }
        //popust
        public List<string> popusti { get; set; }
        public int[] kolicina { get; set; }

    }
}
