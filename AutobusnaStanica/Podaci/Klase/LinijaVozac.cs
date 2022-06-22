using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Podaci.Klase
{
    public class LinijaVozac
    {
        public int LinijaID { get; set; }
        public Linija Linija{ get; set; }
        public int VozacID { get; set; }
        public Vozac Vozac { get; set; }
    }
}
