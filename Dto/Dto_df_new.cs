using System;
using System.Collections.Generic;
using System.Text;

namespace FMReports.Dto
{
    public class Dto_df_new
    {
        public float MaxValue_in_Row_less_MTM_times_10percent { get; set; }
        public float MinValue_in_Row_less_MTM_times_10percent { get; set; }
        public float Mtm_AUD { get; set; }
        public float Collateral_AUD { get; set; }
        public float Net_Exposure_AUD { get; set; }
        public float RCL_AUD { get; set; }
        public float IMR_AUD { get; set; }
        public float LIMITS { get; set; }

    }
}
