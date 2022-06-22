using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Podaci.Klase
{
    public class Stajalista
    {
        [Key]
        public int StajalistaID { get; set; }

        public Linija Linija { get; set; }
        public int LinijaID { get; set; }
        
        public Grad Grad { get; set; }
        public int GradID { get; set; }

        public string SatnicaStizanja { get; set; } //u koliko sati stize bus na ovu stanicu od grada polaska
        public int RedniBrojStajalista { get; set; }

    }
}
