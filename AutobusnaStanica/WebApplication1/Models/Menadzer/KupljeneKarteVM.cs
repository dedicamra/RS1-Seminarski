﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Menadzer
{
    public class KupljeneKarteVM
    {
        public class Row
        {
            public int KartaID { get; set; }
            public string PolazisteNaziv { get; set; }
            public string DolazisteNaziv { get; set; }
            public string TipKarte { get; set; }
            public string OznakaLinije { get; set; }
            public string DatumPolaska { get; set; }
            public string DatumDolaska { get; set; }
            public string DatumKupovineKarte { get; set; }
            public string VrstaPopusta { get; set; }
            public float Cijena { get; set; }
            public bool IsAktivna{ get; set; }
        }
        public List<Row> redovi { get; set; }
        public List<SelectListItem> listTipKarte{ get; set; }
        public int? TipKarteId { get; set; }
        public DateTime? DatumPolaska { get; set; }
        public DateTime? DatumKupovine{ get; set; }
    }
}
