using DataGenerator.Data.DataModels.Infrastructure;
using System.Threading.Tasks;

namespace DataGenerator.Business.SampleData.Infrastructure
{
    /// <summary>
    /// Provides methods to load sample data and publish it to the data layer.
    /// </summary>
    public interface ISampleDataService
    {
        /// <summary>
        /// Clears all items from the repository and loads the sample data.
        /// </summary>
        Task Init(IDataFileReader fileReader);
    }
}