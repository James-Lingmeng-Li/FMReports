using System;
using System.Collections.Generic;
using System.Text;

namespace FMReports.Models
{
    public class SpotVol
    {
        public string Deal_Id { get; set; }
        public string Counterparty { get; set; }
        public string Folder { get; set; }
        public string Dealer { get; set; }
        public string Product { get; set; }
        public string Status { get; set; }
        public string Trade_Date { get; set; }
        public string Spot_Bump { get; set; }
        public string Spot_Value { get; set; }
        public string Vol_Bump { get; set; }
        public string Vol_Value { get; set; }
        public string Mtm { get; set; }
    }
}
