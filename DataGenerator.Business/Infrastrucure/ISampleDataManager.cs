using System.Threading.Tasks;

namespace DataGenerator.Business.Infrastructure
{
    /// <summary>
    /// Provides methods to load sample data and publish it to the data layer.
    /// </summary>
    public interface ISampleDataManager
    {
        /// <summary>
        /// Clears all items from the repository and loads the sample data.
        /// </summary>
        Task Init();
    }
}