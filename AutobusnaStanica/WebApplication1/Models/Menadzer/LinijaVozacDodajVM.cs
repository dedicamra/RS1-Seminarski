using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Menadzer
{
    public class LinijaVozacDodajVM
    {
        public List<SelectListItem> Vozaci{ get; set; }
        public int VozacID{ get; set; }
        public int LinijaID{ get; set; }
    }
}
