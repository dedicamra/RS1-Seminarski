using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.VoziloAngular
{
    public class VoziloPrikazVM
    {
        public class Row
        {
            public int id { get; set; }
            public string oznakaVozila { get; set; }
            public string registracijskiBroj { get; set; }
            public int maxBrojSjedista { get; set; }
           
            public string datumZadnjegServisa { get; set; }
        }

        public List<Row> vozila { get; set; }
        public string pretraga { get; set; }
    }
}
