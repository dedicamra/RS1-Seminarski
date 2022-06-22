using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.VoziloAngular
{
    public class VoziloUrediVM
    {
        public int voziloID { get; set; }
        public string oznakaVozila { get; set; }
        public string registracijskiBroj { get; set; }
        public int maxBrojSjedista { get; set; }
        public string datumZadnjegServisa { get; set; }
    }
}
