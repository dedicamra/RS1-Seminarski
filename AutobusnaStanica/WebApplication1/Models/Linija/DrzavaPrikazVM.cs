using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Linija
{
    public class DrzavaPrikazVM
    {
        public class Row
        {
            public int DrzavaID { get; set; }
            public string Naziv { get; set; }
        }
        public List<Row> Drzave;
        public string pretraga;
    }
}
