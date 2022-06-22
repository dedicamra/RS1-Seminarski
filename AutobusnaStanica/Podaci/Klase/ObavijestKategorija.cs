using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class ObavijestKategorija
    {
        [Key]
        public int ObavijestKategorijaID { get; set; }
        public string Naziv { get; set; }
    }
}
