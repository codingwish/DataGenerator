using DataGenerator.Business.PersonDetailsGeneration.Infrastructure;
using DataGenerator.Business.SampleData.Infrastructure;
using DataGenerator.Core.Container;
using DataGenerator.Data.DataAccess.Infrastructure;
using DataGenerator.Data.DataModels;
using DataGenerator.Data.DataModels.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.UI.CLI
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
                ISampleDataService manager = ComponentFactory.CreateSampleDataService(_dataLayer);
                Task initializationTask = Task.Run(() => manager.Init(ComponentFactory.CreateDataFileReader()));
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
                IPersonDetailsGeneratorOptions options = ComponentFactory.CreatePersonDetailsGeneratorOptions
                    (
                        ComponentFactory.CreateCultureValueGenerator(),
                        _dataLayer.SelectRecords<MaleName>("MaleName").Result.ToList<ICultureValue>(),
                        _dataLayer.SelectRecords<FemaleName>("FemaleName").Result.ToList<ICultureValue>(),
                        _dataLayer.SelectRecords<LastName>("LastName").Result.ToList<ICultureValue>()
                    );
                var generator = ComponentFactory.CreatePersonDetailsGenerator(options);
                var personDetails = generator.Get(IsoCode.DE, Gender.Male);
                Console.WriteLine($"{personDetails.FirstName} {personDetails.LastName}");
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
                Console.WriteLine("Enter connection string or /a to cancel:");
                string connectionString = Console.ReadLine();
                if (connectionString == "/a")
                {
                    return null;
                }
                IDataLayer dataLayer = ComponentFactory.CreateDataLayer();
                bool result = Connect(dataLayer, connectionString);
                while (result == false)
                {
                    Console.WriteLine("Enter connection string or /a to cancel:");
                    connectionString = Console.ReadLine();
                    if (connectionString == "/a")
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
                    throw new ApplicationException("No database connection.");
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
