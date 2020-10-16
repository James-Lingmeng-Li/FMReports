using System;
using System.Collections.Generic;
using System.Text;

namespace FMReports.Models
{
    public class Credit
    {
        //--obtain the mappings file that helps map the name in the collateral reports to the FinMech Shortname--
        public string FM_Short_Name { get; set; }
        public string OTM { get; set; }
    }
}
