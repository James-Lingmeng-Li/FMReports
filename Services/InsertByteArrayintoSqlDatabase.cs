using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FMReports.Services
{
    public class InsertByteArrayintoSqlDatabase
    {
        public static void StoreByteArrayintoSqlDatabase(byte[] result)
        {
            byte[] byteData = result;
            var strData = System.Text.Encoding.UTF8.GetString(byteData);

            using (SqlConnection sqlconnection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=fx_portal_test2;Trusted_Connection=True;MultipleActiveResultSets=true;"))
            {
                sqlconnection.Open();
                // create table if not exists 
                string insertXmlQuery = @"Insert Into [FinMechanicsReports] (Id,CsvData,Created) Values(3,@CsvData,getdate())";

                // Insert Byte [] Value into Sql Table by SqlParameter
                SqlCommand insertCommand = new SqlCommand(insertXmlQuery, sqlconnection);
                SqlParameter sqlParam = insertCommand.Parameters.AddWithValue("@CsvData", strData);
                sqlParam.DbType = DbType.String;
                

                insertCommand.ExecuteNonQuery();
            }
        }
    }
}
