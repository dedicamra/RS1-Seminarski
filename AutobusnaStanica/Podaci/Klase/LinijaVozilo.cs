using System;
using System.Collections.Generic;
using System.Text;

namespace Podaci.Klase
{
    public class LinijaVozilo
    {
        public int LinijaID { get; set; }
        public Linija Linija { get; set; }
        public int VoziloID { get; set; }
        public Vozilo Vozilo { get; set; }
    }
}
