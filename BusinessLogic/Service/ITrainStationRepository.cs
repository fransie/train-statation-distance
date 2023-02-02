using System.Collections.Generic;
using BusinessLogic.Model;

namespace BusinessLogic.Service;

public interface ITrainStationRepository
{
    /// <summary>
    /// Gets all train stations.
    /// </summary>
    /// <returns>Collection of train stations</returns>
    IReadOnlyCollection<TrainStation> GetAll();

    /// <summary>
    /// Gets the train stations corresponding to the provided DS100Code.
    /// </summary>
    /// <param name="ds100Code">A short identifier with 2 to 6 characters denoting a train station.</param>
    /// <returns></returns>
    TrainStation GetByDs100Code(string ds100Code);
}