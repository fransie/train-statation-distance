using System;
using BusinessLogic.Model;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Service;

public class DistanceCalculationService : IDistanceCalculationService
{
    private readonly ITrainStationRepository _trainStationRepository;
    private readonly ILogger<IDistanceCalculationService> _logger;

    public DistanceCalculationService(ITrainStationRepository trainStationRepository,
        ILogger<IDistanceCalculationService> logger)
    {
        _trainStationRepository = trainStationRepository;
        _logger = logger;
    }

    public DistanceCalculation? CalculateDistance(string from, string to)
    {
        TrainStation fromTrainStation;
        TrainStation toTrainStation;
        try
        {
            fromTrainStation = _trainStationRepository.GetByDs100Code(from.ToUpper());
            toTrainStation = _trainStationRepository.GetByDs100Code(to.ToUpper());
        }
        catch (ArgumentException)
        {
            _logger.LogWarning($"Either \"{from.ToUpper()}\" or \"{to.ToUpper()}\" is an invalid DS100Code.");
            return null;
        }

        var distance = CalculateHaversineDistanceInKm(fromTrainStation, toTrainStation);

        var distanceCalculation = new DistanceCalculation
        {
            From = fromTrainStation.Name,
            To = toTrainStation.Name,
            Distance = distance,
            Unit = "km"
        };
        return distanceCalculation;
    }


    // The Haversine Formula calculates the distance between to point on the earth.
    // This implementation is heavily inspired by https://de.martech.zone/calculate-great-circle-distance/.
    private static int CalculateHaversineDistanceInKm(TrainStation from, TrainStation to)
    {
        var theta = from.Longitude - to.Longitude;
        var distance = 60 * 1.1515 * (180 / Math.PI) * Math.Acos(
            Math.Sin(from.Latitude * (Math.PI / 180)) * Math.Sin(to.Latitude * (Math.PI / 180)) +
            Math.Cos(from.Latitude * (Math.PI / 180)) * Math.Cos(to.Latitude * (Math.PI / 180)) *
            Math.Cos(theta * (Math.PI / 180))
        );
        return (int)Math.Round(distance * 1.609344);
    }
}