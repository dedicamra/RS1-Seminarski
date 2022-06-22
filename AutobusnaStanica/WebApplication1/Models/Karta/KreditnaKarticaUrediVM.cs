using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Karta
{
    public class KreditnaKarticaUrediVM
    {
        public int KreditnaKarticaID { get; set; }
        public string BrojKartice { get; set; }
        public string ImeVlasnika { get; set; }
        public int VerKod { get; set; }
        public string DatumIsteka { get; set; }
        public string KupacID { get; set; }
    }
}
