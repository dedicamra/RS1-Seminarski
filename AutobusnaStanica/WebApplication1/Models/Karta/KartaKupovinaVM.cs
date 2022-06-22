using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Karta
{
    public class KartaKupovinaVM
    {
        public float Cijena { get; set; }
        public float CijenaBezPopusta { get; set; }
        public string DatumPolaska { get; set; }
        public string DatumDolaska { get; set; }
        public string LinijaPolaziste { get; set; }
        public string LinijaDolaziste { get; set; }
        public string OznakaLinije { get; set; }
        public string PolazisteNaziv { get; set; }
        public string DolazisteNaziv { get; set; }
        public string TipKarte { get; set; }
        public int PolazisteID { get; set; }
        public int DolazisteID { get; set; }
        public int TipKarteID { get; set; }
        public List<SelectListItem> Kartice { get; set; }
        public int KarticeID { get; set; }
        public string BrojKarticeNova { get; set; }
        public string ImeVlasnikaNova { get; set; }
        public int VerKodNova { get; set; }
        public string DatumIstekaNova { get; set; }
        public bool SpasiKarticu { get; set; }
        //popust
        public List<string> popusti { get; set; }
        public int[] kolicina { get; set; }
    }
}
