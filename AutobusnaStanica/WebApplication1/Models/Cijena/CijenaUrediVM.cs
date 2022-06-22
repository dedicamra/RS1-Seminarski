using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Cijena
{
    public class CijenaUrediVM
    {
        public int CijenaID { get; set; }

        public int GradPolaskaGradID { get; set; }
        public string GradPolaska { get; set; }

        public int GradDolaskaGradID { get; set; }
        public string GradDolaska { get; set; }

        public float JednosmijernaKartaCijena { get; set; }
        public float PovratnaKartaCijena { get; set; }

        public List<SelectListItem> Gradovi; 
    }
}
