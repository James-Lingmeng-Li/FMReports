using System;
using System.Collections.Generic;
using System.Text;

namespace FMReports.Dto
{
    public class Dto_df_combined
    {
        public string CounterParty { get; set; }
        public string CurrencyPair { get; set; }
        public string InstrType { get; set; }
        public float Mtm { get; set; }
        public float Spot { get; set; }
    }
}
