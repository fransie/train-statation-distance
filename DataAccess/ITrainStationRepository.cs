using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Model;

namespace DataAccess;

public interface ITrainStationRepository
{
    /// <summary>
    /// Gets all train stations.
    /// </summary>
    /// <returns>Collection of train stations</returns>
    Task<IReadOnlyCollection<TrainStation>> GetAllAsync();

    /// <summary>
    /// Gets the train stations corresponding to the provided DS100Code.
    /// </summary>
    /// <param name="ds100Code">A short identifier with 2 to 6 characters denoting a train station.</param>
    /// <returns></returns>
    Task<TrainStation> GetByDs100CodeAsync(string ds100Code);
}