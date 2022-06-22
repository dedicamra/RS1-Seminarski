using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Feedback
{
    public class FeedbackPrikazVM
    {
        public class Feedback
        {
            public int Id { get; set; }
            public string KupacIme{ get; set; }
            public string KupacPrezime { get; set; }
            public int KupacId { get; set; }
            public string DatumKreiranja { get; set; }
            public DateTime Datum{ get; set; }
            public string Sadrzaj { get; set; }
            public bool IsDeleted { get; set; }
        }
        public List<Feedback> feedbacks { get; set; }
        public int moje { get; set; }
    }
}
