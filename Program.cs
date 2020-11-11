using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
            var localost = "10.1.2.12";
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

            //Available Report List from FM

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
            string dealSummaryEndpointURl = "https://localhost:5001/api/Risk/ReadDealSummary";
            string spotVolEndpointURl = "https://localhost:5001/api/Risk/ReadSpotVol";
            string reportName = "DealSummaryReport";
            string Id = string.Empty;
            DateTime now = DateTime.Now;
            string nowString = now.ToString("yyyy-mm-dd hh:mm:ss");
            string reportDate = "2020-10-10 18:00:00";
            ReportOutput output = await reportingClient.getBatchReportsAsync(reportName, reportDate);
            byte[] result = output.Result;


            //close the connection
            Console.WriteLine("done");
            transport.Close();
            try
            {
                Id = FMReports.Services.InsertByteArrayintoSqlDatabase.StoreByteArrayintoSqlDatabase(result,reportName);
                Console.WriteLine(reportName+" data has been inserted to SQL database " + Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            ////try
            ////{
            ////    FileStream fs = new FileStream("C:\\FM\\" + reportName + ".csv", FileMode.Create, FileAccess.ReadWrite);
            ////    BinaryWriter bw = new BinaryWriter(fs);
            ////    bw.Write(result);
            ////    Console.WriteLine("csv file created");
            ////}
            ////catch (Exception e)
            ////{
            ////    Console.WriteLine(e.Message);
            ////}
            ///


            try
            {
                if (reportName == "DealSummaryReport")
                {
                    await PostRequest(dealSummaryEndpointURl, Id);
                }
                else
                {
                    await PostRequest(spotVolEndpointURl, Id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public async static Task PostRequest(string url, string Id)
        {

            using (HttpClient client = new HttpClient())
            {


                JObject o = JObject.FromObject(new
                {
                    FinMechId = Id
                });


                var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
                {
                    Content = new StringContent(o.ToString(), Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                var contentJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(contentJson);
                }

                Console.WriteLine("Request has been sent to " + url +"successfully with report ID is:" + Id);

            }
        }
    }
}