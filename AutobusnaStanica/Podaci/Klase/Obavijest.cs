using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text;
using WebApplication1;

namespace Podaci.Klase
{
    public class Obavijest
    {
        [Key]
        public int ObavijestID { get; set; }
        public string Naslov { get; set; }
        public string Podnaslov { get; set; }
        public string Opis { get; set; }
        public byte[] Slika { get; set; }
        public string DatumObjave{ get; set; }
        public int ObavijestKategorijaID { get; set; }
        public ObavijestKategorija ObavijestKategorija { get; set; }
    }
}
