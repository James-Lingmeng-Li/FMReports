using System;
using System.Collections.Generic;
using System.Text;

namespace FMReports.Models
{
    public class FXCollateralReport
    {
        public string Customer { get; set; }
        public float Amount_AUD { get; set; }
        public float Amount_USD { get; set; }
        public string Comment { get; set; }
    }
}
