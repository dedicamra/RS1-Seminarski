using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Podaci.Klase
{
    public class Vozilo
    {
        [Key]
        public int VoziloID { get; set; }
        public string OznakaVozila { get; set; }
        public string RegistracijskiBroj { get; set; }
        public int MaxBrojSjedista { get; set; }
        public string DatumZadnjegServisa { get; set; }
        public  ICollection<LinijaVozilo> Linija { get; set; }

    }
}
