using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Linija
{
    public class StajalisteUrediVM
    {
        public int StajaistaID { get; set; }

        public int LinijaID { get; set; }
        public string Linija { get; set; }

        public int GradID { get; set; }
        public string Grad { get; set; }

        public string SatnicaStizanja { get; set; } //u koliko sati stize bus na ovu stanicu od grada polaska
        public int RedniBrojStajalista { get; set; }

        public List<SelectListItem> Gradovi;
        public int _linijaID { get; set; }
        public int _stajalisteID { get; set; }

    }
}
