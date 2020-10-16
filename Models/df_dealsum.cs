using System;
using System.Collections.Generic;
using System.Text;

namespace FMReports.Models
{
    public class df_dealsum
    {
        public string Instr_Type { get; set; }
        public string Deal_Number { get; set; }
        public int Deal_Index { get; set; }
        public string Deal_Status { get; set; }
        public string Liquidation_Status { get; set; }
        public string Is_Terminated { get; set; }
        public string Deal_Type { get; set; }
        public string Exercise_Type { get; set; }
        public string Option_Type { get; set; }
        public string Currency_Pair { get; set; }
        public string Trade_Date { get; set; }
        public string Premium_Date { get; set; }
        public string Maturity_Date { get; set; }
        public string Delivery_Date { get; set; }
        public string Folder { get; set; }
        public string Counterparty { get; set; }
        public string Notional_Amount1 { get; set; }
        public string Notional_Amount2 { get; set; }
        public string Strike { get; set; }
        public string Barrier_Level { get; set; }
        public string Barrier_Direction { get; set; }
        public string Barrier_Leve2 { get; set; }
        public string Barrier_Direction2 { get; set; }
        public string Premium_Quote { get; set; }
        public string Premium_Convention { get; set; }
        public string Spot { get; set; }
        public string Domestic_DF { get; set; }
        public string Foreign_DF { get; set; }
        public string Implied_Volatility { get; set; }
        public string Forward { get; set; }
        public string Premium { get; set; }
        public string Mtm { get; set; }
        public string Eoy_Mtm { get; set; }
        public string Cash_Settlement { get; set; }
        public string Pnl { get; set; }
        public string Forward_Delta { get; set; }
        public string Forward_Delta_Value { get; set; }
        public string Spot_Delta { get; set; }
        public string Spot_Delta_Value { get; set; }
        public string Premium_Adjusted_Delta { get; set; }
        public string Gamma { get; set; }
        public string Theta { get; set; }
        public string Vega { get; set; }
        public string Rhop { get; set; }
        public string Rhof { get; set; }
        public string Vanna { get; set; }
        public string Volga { get; set; }
        public string Pv01_Currency_1 { get; set; }
        public string Pv01_Currency_2 { get; set; }
        public string Simulated_Delta { get; set; }
        public string Utilised_Points { get; set; }
    }
}
