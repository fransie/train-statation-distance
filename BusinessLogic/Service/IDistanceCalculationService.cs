using BusinessLogic.Model;

namespace BusinessLogic.Service;

public interface IDistanceCalculationService
{
    /// <summary>
    /// Returns the air-line distance between two train stations.
    /// </summary>
    /// <param name="from">Start train station</param>
    /// <param name="to">End train station</param>
    /// <returns>The DistanceCalculation or null if one of the argument codes is invalid.</returns>
    DistanceCalculation? CalculateDistance(string from, string to);
}