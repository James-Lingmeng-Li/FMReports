using FMReports.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMReports.Funcs
{
    public class Calculations
    {
        public static List<string> SpotVolFunc1(List<SpotVol> deals)
        {
            var spotBumpList = new List<string>();




            return spotBumpList;
        }
        public static List<string> SpotVolFunc2(List<SpotVol> deals)
        {
            var volBumpList = new List<string>();




            return volBumpList;
        }
        public static List<string> SpotVolFunc3(List<SpotVol> deals)
        {
            var list_tups = new List<string>();




            return list_tups;
        }
        public static (List<string>, List<string>, List<string>, List<string>) DealSumFunc1(List<SpotVol> deals)
        {
            var df_summary = new List<string>();
            var df_max_value = new List<string>();
            var df_min_value = new List<string>();
            var df_combained = new List<string>();




            return (df_summary, df_max_value,df_min_value, df_combained);
        }
        public static List<string> DealSumFunc2(List<SpotVol> deals)
        {
            var df_summary = new List<string>();




            return df_summary;
        }
    }
}
