using System;
using System.Collections.Generic;
using System.Text;

namespace Podaci.Klase
{
    public class Linija
    {
        public int LinijaID { get; set; }
        public string OznakaLinije { get; set; }

        public int GradPolaskaGradID { get; set; }
        public Grad GradPolaska { get; set; }

        public int GradDolaskaGradID { get; set; }
        public Grad GradDolaska { get; set; }


        public bool Ponedjeljak { get; set; }
        public bool Utorak { get; set; }
        public bool Srijeda { get; set; }
        public bool Cetvrtak { get; set; }
        public bool Petak { get; set; }
        public bool Subota { get; set; }
        public bool Nedjelja { get; set; }

        public string VrijemePolaska { get; set; }
        public string VrijemeDolaska { get; set; }

        public string[] DaniUSedmici { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<LinijaVozilo> Vozilo { get; set; }
        public virtual ICollection<LinijaVozac> Vozac { get; set; }
    }
}
