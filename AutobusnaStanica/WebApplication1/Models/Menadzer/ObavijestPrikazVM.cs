using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Menadzer
{
    public class ObavijestPrikazVM
    {
        public class Row
        {
            public int ObavijestID { get; set; }
            public string Naslov { get; set; }
            public string Podnaslov { get; set; }
            public string Opis { get; set; }
            public string Slika { get; set; }
            public string ObavijestKategorija { get; set; }
            public string DatumObjave { get; set; }
        }
        public List<Row> obavijesti;
    }
}
