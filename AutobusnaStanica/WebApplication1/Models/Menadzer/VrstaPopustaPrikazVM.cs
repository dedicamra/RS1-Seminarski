using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.VrstaPopusta
{
    public class VrstaPopustaPrikazVM
    {
        public int VrstaPopustaID { get; set; }
        public string Naziv { get; set; }
        public float Iznos { get; set; }
    }
}
