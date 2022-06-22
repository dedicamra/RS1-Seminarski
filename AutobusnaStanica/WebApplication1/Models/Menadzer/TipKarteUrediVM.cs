using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.TipKarte
{
    public class TipKarteUrediVM
    {
        public int TipKarteID { get; set; }
        public string Naziv { get; set; }
        public bool IsAktivan{ get; set; }
    }
}
