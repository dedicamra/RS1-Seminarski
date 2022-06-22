using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Podaci.Klase
{
    public class KreditnaKartica
    {
        [Key]
        public int KreditnaKarticaID { get; set; }
        public string BrojKartice { get; set; }
        public string DatumIsteka { get; set; }
        public string ImeVlasnikaKartice { get; set; }
        public int VerifikacijskiKod { get; set; }
        public virtual Kupac Kupac { get; set; }
        public bool IsAktivna { get; set; }
    }
}
