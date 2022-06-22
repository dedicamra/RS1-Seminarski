using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1;

namespace Podaci.Klase
{
    public class Grad
    {
        public int GradID { get; set; }
        public string Naziv  { get; set; }
        public int DrzavaID { get; set; }
        public Drzava Drzava { get; set; }
        public bool IsDeleted { get; set; }
    }
}
