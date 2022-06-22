using System;
using System.Collections.Generic;
using System.Text;

namespace Podaci.Klase
{
    public class Cijena
    {
        public int CijenaID { get; set; }

        public int GradPolaskaGradID { get; set; }
        public Grad GradPolaska { get; set; }

        public int GradDolaskaGradID { get; set; }
        public Grad GradDolaska { get; set; }

        public float JednosmijernaKartaCijena { get; set; }
        public float PovratnaKartaCijena { get; set; }
    }
}
