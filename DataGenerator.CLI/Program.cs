using DataGenerator.Business;
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
            GetDataLayer();
            Init();
            //manager.CreateSampleDataItem<LastName>(new System.Collections.Generic.List<object>() { "Test" }, IsoCode.FR);
            
            var lastNames = _dataLayer.SelectRecords<LastName>("LastName");
            var result = lastNames.Result.Where(ln => ln.Value == "Test");
            if (result.FirstOrDefault() != null )
            {
                Console.WriteLine(result.FirstOrDefault().Value);
            }
            //foreach (var lastName in lastNames.Result)
            //{
            //    Console.WriteLine(lastName.Value);
            //}
        }

        private static void Init()
        {
            var manager = new SampleDataManager(_dataLayer);
            var t = Task.Run(() => manager.Init());
            t.Wait();
        }

        private static void GetDataLayer()
        {
            try
            {
                Console.WriteLine("Enter connection string:");
                string connectionString = Console.ReadLine();
                var result = Connect(connectionString);
                while (result == false)
                {
                    Console.WriteLine("Enter connection string or E to exit:");
                    connectionString = Console.ReadLine();
                    result = connectionString == "E" || Connect(connectionString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static bool Connect(string connectionString)
        {
            try
            {
                IDataLayer dataLayer = new CosmosDataLayer();
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
