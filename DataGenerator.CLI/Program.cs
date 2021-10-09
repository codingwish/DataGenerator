using DataGenerator.Business;
using DataGenerator.Business.Infrastructure;
using DataGenerator.Data;
using DataGenerator.Data.DataAccess;
using DataGenerator.Data.Infrastructure;
using DataGenerator.Data.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.CLI
{
    class Program
    {
        private static IDataLayer _dataLayer;

        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                ParseCommand(args[0]);
            }
            else
            {
                Console.WriteLine("Please enter command. Type /? to get help.");
                ParseCommand(Console.ReadLine());
            }
            //Init();
            //manager.CreateSampleDataItem<LastName>(new System.Collections.Generic.List<object>() { "Test" }, IsoCode.FR);

            var lastNames = _dataLayer.SelectRecords<LastName>("LastName");
            var result = lastNames.Result.Where(ln => ln.Value == "Müller");
            if (result.FirstOrDefault() != null)
            {
                Console.WriteLine(result.FirstOrDefault().Value);
            }
        }

        private static void ParseCommand(string input)
        {
            if (input == "/i")
            {
                InitSampleData();
            }
            else if (input == "/?")
            {
                DisplayHelp();
            }
            else if (input == "/q")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Unknwon command. Type /? to list commands.");
                ParseCommand(Console.ReadLine());
            }
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("/? displays this help menu.");
            Console.WriteLine("/i clears the repository and loads the sample data.");
            Console.WriteLine("/q will exit the application.");
        }

        private static void InitSampleData()
        {
            if (_dataLayer == null)
            {
                GetDataLayer();
                Console.WriteLine("Failed to connect to the repository.");
            }
            ISampleDataManager manager = new SampleDataManager(_dataLayer);
            var t = Task.Run(() => manager.Init());
            t.Wait();
        }

        private static bool GetDataLayer()
        {
            try
            {
                Console.WriteLine("Enter connection string:");
                string connectionString = Console.ReadLine();
                IDataLayer dataLayer = new CosmosDataLayer();
                var result = Connect(dataLayer, connectionString);
                while (result == false)
                {
                    Console.WriteLine("Enter connection string or /q to exit:");
                    connectionString = Console.ReadLine();
                    result = connectionString == "/q" || Connect(dataLayer, connectionString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static bool Connect(IDataLayer dataLayer, string connectionString)
        {
            try
            {
                if (!dataLayer.Connect(connectionString))
                {
                    throw new ApplicationException("Error while trying to connect to the database.");
                }
                _dataLayer = dataLayer;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
