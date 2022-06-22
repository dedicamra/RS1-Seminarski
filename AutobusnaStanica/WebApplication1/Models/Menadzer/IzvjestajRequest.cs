using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Menadzer
{
    public class IzvjestajRequest
    {
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }
        public bool IspravanInput { get; set; }
    }
}
