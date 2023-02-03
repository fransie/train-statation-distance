using BusinessLogic.Model;
using TrainStationDistance.Model;

namespace TrainStationDistance.Mapping;

public interface IDtoMapper
{
    /// <summary>
    ///     Maps a DistanceCalculation object to the corresponding DistanceCalculationDto object.
    /// </summary>
    /// <param name="distanceCalculation">DistanceCalculation to be mapped.</param>
    /// <returns>Mapped DistanceCalculationDto.</returns>
    public DistanceCalculationDto MapToDto(DistanceCalculation distanceCalculation);
}