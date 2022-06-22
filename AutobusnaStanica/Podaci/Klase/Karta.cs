using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Podaci.Klase
{
    public class Karta
    {
        [Key]
        public int KartaID { get; set; }
        public string DatumKupovine { get; set; }
        public string DatumPolaska { get; set; }
        public string DatumDolaska { get; set; }
        public TipKarte TipKarte { get; set; }
        public int TipKarteID { get; set; }
        public float Cijena { get; set; }
        public VrstaPopusta VrstaPopusta{ get; set; }
        public int VrstaPopustaID { get; set; }
        public Stajalista Polaziste{ get; set; }
        public int? PolazisteID { get; set; }
        public Stajalista Dolaziste { get; set; }
        public int? DolazisteID { get; set; }
        public bool IsAktivna { get; set; }
        public string NazivLinije{ get; set; }
        public int? KKarticaID { get; set; }
        public KreditnaKartica KKartica { get; set; }
        [ForeignKey("Kupac_ID")]
        public string Kupac_ID { get; set; }
        public virtual Kupac Kupac { get; set; }
    }
}
