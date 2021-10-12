using DataGenerator.Business;
using DataGenerator.Business.CultureValueGeneration;
using DataGenerator.Business.PersonalDataGeneration;
using DataGenerator.Business.PersonalDataGeneration.Infrastructure;
using DataGenerator.Business.SampleData.Infrastructure;
using DataGenerator.Data.DataAccess;
using DataGenerator.Data.DataAccess.Infrastructure;
using DataGenerator.Data.DataModels;
using DataGenerator.Data.DataModels.Infrastructure;
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
        }

        private static void ParseCommand(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input) || input.Length < 2)
                {
                    throw new ArgumentNullException(nameof(input));
                }
                switch (input.Substring(0, 2))
                {
                    case "/c":
                        CreateRandomName();
                        break;
                    case "/i":
                        InitSampleData();
                        break;
                    case "/?":
                        ShowHelp();
                        break;
                    case "/q":
                        Environment.Exit(0);
                        break;
                    default:
                        ShowCommandPrompt();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ShowCommandPrompt();
            }
        }

        private static void ShowCommandPrompt()
        {
            Console.WriteLine("Please enter command. Type /? to get help.");
            ParseCommand(Console.ReadLine());
        }

        private static void ShowHelp()
        {
            Console.WriteLine("/? displays this help menu.");
            Console.WriteLine("/c creates a randon name.");
            Console.WriteLine("/i clears the repository and loads the sample data.");
            Console.WriteLine("/q will exit the application.");
            ShowCommandPrompt();
        }

        private static void InitSampleData()
        {
            try
            {
                _dataLayer = _dataLayer == null ? GetDataLayer() : _dataLayer;
                if (_dataLayer == null)
                {
                    throw new ApplicationException("Can't connect to repository.");
                }
                ISampleDataService manager = new SampleDataService(_dataLayer);
                var initializationTask = Task.Run(() => manager.Init());
                initializationTask.Wait();
                ShowCommandPrompt();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                ShowCommandPrompt();
            }
        }

        private static void CreateRandomName()
        {
            try
            {
                _dataLayer = _dataLayer == null ? GetDataLayer() : _dataLayer;
                if (_dataLayer == null)
                {
                    throw new ApplicationException("Can't connect to repository.");
                }
                var options = new PersonDataGeneratorSettings
                    (
                        new CultureValueGenerator(),
                        _dataLayer.SelectRecords<MaleName>("MaleName").Result.ToList<ICultureValue>(),
                        _dataLayer.SelectRecords<FemaleName>("FemaleName").Result.ToList<ICultureValue>(),
                        _dataLayer.SelectRecords<LastName>("LastName").Result.ToList<ICultureValue>()
                    );
                var generator = new PersonDataGenerator(options);
                Console.WriteLine(generator.GetFullName(IsoCode.DE, Gender.Male));
                ShowCommandPrompt();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                ShowCommandPrompt();
            }
        }

        private static IDataLayer GetDataLayer()
        {
            try
            {
                Console.WriteLine("Enter connection string or /c to cancel:");
                string connectionString = Console.ReadLine();
                if (connectionString == "/c")
                {
                    return null;
                }
                IDataLayer dataLayer = new CosmosDataLayer();
                bool result = Connect(dataLayer, connectionString);
                while (result == false)
                {
                    Console.WriteLine("Enter connection string or /c to cancel:");
                    connectionString = Console.ReadLine();
                    if (connectionString == "/c")
                    {
                        return null;
                    }
                    result = Connect(dataLayer, connectionString);
                }
                return dataLayer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return GetDataLayer();
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
