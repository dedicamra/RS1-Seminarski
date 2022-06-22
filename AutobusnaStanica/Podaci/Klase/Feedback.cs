using System;
using System.Collections.Generic;
using System.Text;

namespace Podaci.Klase
{
    public class Feedback
    {
        public int Id { get; set; }
        public virtual Kupac Kupac { get; set; }
        //public int KupacId { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public string Sadrzaj { get; set; }
        public bool IsDeleted { get; set; }
    }
}
