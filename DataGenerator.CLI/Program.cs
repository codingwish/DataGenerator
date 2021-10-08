using DataGenerator.Data;
using DataGenerator.Data.DataAccess;
using DataGenerator.Data.Infrastructure;
using DataGenerator.Data.Models;
using System;

namespace DataGenerator.CLI
{
    class Program
    {
        private static IDataLayer _dataLayer;

        static void Main(string[] args)
        {
            GetDataLayer();
            var items =_dataLayer.SelectRecords<LastName>("LastName");
            foreach (var item in items.Result)
            {
                Console.WriteLine(item.Name);
            }
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
