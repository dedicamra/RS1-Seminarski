using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TempAppForReportDesign
{
    public class KartaRow
    {
        /*public int KartaID { get; set; }*/
        public string PolazisteNaziv { get; set; }
        public string DolazisteNaziv { get; set; }
        public string TipKarte { get; set; }
        public string OznakaLinije { get; set; }
        public string DatumPolaska { get; set; }
        public string DatumDolaska { get; set; }
        public string DatumKupovineKarte { get; set; }
        public string VrstaPopusta { get; set; }
        public float Cijena { get; set; }
        public bool IsAktivna { get; set; }

        public static List<KartaRow> Get()
        {
            return new List<KartaRow>();
        }
    }
}