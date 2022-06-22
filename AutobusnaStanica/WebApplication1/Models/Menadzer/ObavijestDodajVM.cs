using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Menadzer
{
    public class ObavijestUrediVM
    {
        public int ObavijestID { get; set; }
        public string Naslov { get; set; }
        public string Podnaslov { get; set; }
        public string Opis { get; set; }
        public string Slika { get; set; }
        public string ObavijestKategorija { get; set; }
        public string DatumObjave { get; set; }  
        public bool IsAktivna{ get; set; }  
        public List<SelectListItem> Kategorije { get; set; }
        public int KategorijaID { get; set; }
    }
}
