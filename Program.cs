using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Transport.Client;

namespace ThirftClientCSharp
{
    class ThriftClientMain
    {
        static async Task Main(string[] args)
        {
            ///Start connetion
            Console.WriteLine("Starting Thrift Client!");
            var localost = "10.1.2.9";
            var client = new TcpClient(localost, 9095);

            /// This is the SSL certificate to be used
            X509Certificate2 certificate = new X509Certificate2("C:/FM/mySrvKeystore.p12", "thrift");
            Thrift.Transport.TTransport transport = new TTlsSocketTransport(client, certificate, false, null, null, SslProtocols.Tls12);
            var protocol = new TBinaryProtocol(transport);

            ///Create multiple TMultiplexedProtocol for each service that is needed
            TMultiplexedProtocol mp = new TMultiplexedProtocol(protocol, "ReportingService");
            var reportingClient = new ReportingService.Client(mp);

            ///the client server is now connected
            Console.WriteLine("connected");

            //Available Reports from FM

            ///string reportName = "FxCashPnLReport";
            ///Sales FXCash P&L ReportASIC Report
            ///Customer MTM Report
            ///SpotVolReport
            ///CptyWiseSpotVolReport
            ///CashflowReport
            ///TarnCashflowReport
            ///PnL Report
            ///DealSummaryReport
            ///SpotVolReport
            ///DealEnquiryReport
            ///CashflowReport

            string reportName = "DealSummaryReport";

            DateTime now = DateTime.Now;
            string nowString = now.ToString("yyyy-mm-dd hh:mm:ss");
            string date = "2020-09-02 18:00:00";
            ReportOutput output = await reportingClient.getBatchReportsAsync(reportName, date);
            byte[] result = output.Result;


            //close the connection
            Console.WriteLine("done");
            transport.Close();
            try
            {
                FMReports.Services.InsertByteArrayintoSqlDatabase.StoreByteArrayintoSqlDatabase(result);
                Console.WriteLine("data has been inserted to SQL databasec");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                FileStream fs = new FileStream("C:\\FM\\" + reportName + ".csv", FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(result);
                Console.WriteLine("csv file created");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        async static void PostRequest(string url, string date)
        {
            DateTime now = DateTime.Now;
            string nowString = now.ToString("yyyy-mm-dd hh:mm:ss");

            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("finMechid","1")
            };

            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsync(url, q))
                {
                    using (HttpContent content = response.Content)
                    {
                        string myContent = await content.ReadAsStringAsync();
                        HttpContentHeaders headers = content.Headers;

                        Console.WriteLine(myContent);
                    }
                }

            }
        }
    }
}