using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FMReports.Services
{
    public class CreateCSVFile
    {
        public static void CreateCsvFile(byte[] result)
        {
            string reportName = "FxCashPnLReport";
            string reportDate = DateTime.Now.ToString("yyyyMMdd");
            FileStream fs = new FileStream("C:\\FM\\"+reportName+reportDate+".csv", FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(result);
            Console.WriteLine("csv file has been created");
        }
    }
}
