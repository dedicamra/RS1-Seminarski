using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Menadzer
{
    public class LinijaVozacPrikazVM
    {
        public class Row
        {
            public int VozacID { get; set; }
            public string ImePrezime { get; set; }
        }
        public int LinijaID { get; set; }
        public string NazivLinije { get; set; }
        public List<Row> redovi { get; set; }
    }
}
