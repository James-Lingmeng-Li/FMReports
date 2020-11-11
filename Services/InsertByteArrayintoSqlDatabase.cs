using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FMReports.Services
{
    public class InsertByteArrayintoSqlDatabase
    {
        public static string StoreByteArrayintoSqlDatabase(byte[] result,string reportName)
        {
            byte[] byteData = result;
            var strData = Encoding.UTF8.GetString(byteData);
           
            

            using (SqlConnection con = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=fx_portal_test;Trusted_Connection=True;MultipleActiveResultSets=true;"))
            using (SqlCommand cmd = new SqlCommand(@"INSERT INTO[dbo].[FinMechanicsReports]([Id],[Created],[BinData],[CsvData],[ReportName],[IsProcessed]) output INSERTED.ID VALUES(@Id, getdate() , @BinData, @CsvData, @ReportName,0)", con))

            {
                var guid = Guid.NewGuid().ToString();
                cmd.Parameters.AddWithValue("@Id", guid);
                cmd.Parameters.AddWithValue("@BinData", byteData);
                cmd.Parameters.AddWithValue("@CsvData", strData);
                cmd.Parameters.AddWithValue("@ReportName", reportName);
                
                con.Open();
                string modified = (string)cmd.ExecuteScalar();
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return guid;
            }
        }


    }
}
