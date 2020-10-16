using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FMReports.Services
{
    public class ReadByteArrayFromSqlDatabase
    {
        public static void RetrieveByteArrayFromSqlDatabase(int id)
        {
            using (SqlConnection sqlconnection = new SqlConnection(@"Data Source=.SQLExpress; 
                Initial Catalog=MorganDB; Integrated Security=SSPI;"))
            {
                sqlconnection.Open();

                string selectQuery = string.Format(@"Select [BinData] From [MyTable] Where ID={0}", id);

                // Read Byte [] Value from Sql Table 
                SqlCommand selectCommand = new SqlCommand(selectQuery, sqlconnection);
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    byte[] byteData = (byte[])reader[0];
                    string strData = Encoding.UTF8.GetString(byteData);
                    Console.WriteLine(strData);
                }
            }
        }

    }
}
