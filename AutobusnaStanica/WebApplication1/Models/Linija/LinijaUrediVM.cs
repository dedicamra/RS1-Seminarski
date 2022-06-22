using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Linija
{
    public class LinijaUrediVM
    {
        public int LinijaID { get; set; }
        public string OznakaLinije { get; set; }

        public int GradPolaskaGradID { get; set; }
        public string GradPolaska { get; set; }

        public int GradDolaskaGradID { get; set; }
        public string GradDolaska { get; set; }


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
        public int VoziloId { get; set; }
        public string Vozilo { get; set; }
        public List<SelectListItem> Gradovi;
        public List<SelectListItem> Vozila;
    }
}
