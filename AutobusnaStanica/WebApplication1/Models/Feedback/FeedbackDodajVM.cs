using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Feedback
{
    public class FeedbackDodajVM
    {
        public int Id { get; set; }
        public string Sadrzaj { get; set; }
        public string Datum { get; set; }
        public string KorisnikImePrezime { get; set; }
    }
}
