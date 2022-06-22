using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Menadzer
{
    public class VoziloUrediVM
    {
        public int VoziloID { get; set; }
        public string OznakaVozila { get; set; }
        public string RegistracijskiBroj { get; set; }
        public int MaxBrojSjedista { get; set; }
        [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        //public DateTime DatumZadnjegServisa { get; set; }
        public string DatumZadnjegServisa { get; set; }
    }
}
