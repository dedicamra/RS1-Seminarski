using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TempAppForReportDesign
{
    public class LinijaRow
    {
        public int LinijaId { get; set; }
        public string Linija { get; set; }
        public string[] DaniUSedmiciArray { get; set; }
        public string DaniUSedmici { get; set; }
        public string StajalistaSatnice { get; set; }

        public List<string> stajalista { get; set; }

        public static List<LinijaRow> Get()
        {
            return new List<LinijaRow>();
        }
    }
}