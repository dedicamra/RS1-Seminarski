using System;
using System.Collections.Generic;
using System.Text;

namespace Podaci.Klase
{
    public class VrstaPopusta
    {
        public int VrstaPopustaID { get; set; }
        public string Naziv { get; set; }
        public float Iznos { get; set; }
        public bool IsAktivan { get; set; }
    }
}
