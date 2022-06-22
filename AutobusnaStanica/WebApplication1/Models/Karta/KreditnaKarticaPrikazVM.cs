using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Karta
{
    public class KreditnaKarticaPrikazVM
    {
        public class KarticaRedovi
        {
            public int kreditnaKarticaID { get; set; }
            public string brojKartice { get; set; }
            public string imeVlasnika { get; set; }
            public int verKod { get; set; }
            public string datumIsteka { get; set; }
        }
        public List<KarticaRedovi> karticeKupca { get; set; }
        public List<SelectListItem> Kartice{ get; set; }
        public int KarticeID{ get; set; }
        public string BrojKarticeNova { get; set; }
        public string ImeVlasnikaNova { get; set; }
        public int VerKodNova { get; set; }
        public string DatumIstekaNova { get; set; }
    }
}
