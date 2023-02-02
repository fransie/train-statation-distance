namespace TrainStationDistance.Model;

/// <summary>
/// Contains the the air-line distance between two German intercity train stations. 
/// </summary>
public record DistanceCalculationDto
{
    /// <summary>
    /// Start train station.
    /// </summary>
    public string From { get; init; }
    
    /// <summary>
    /// End train station.
    /// </summary>
    public string To { get; init; }
    
    /// <summary>
    /// Air-line distance between the two train stations.
    /// </summary>
    public int Distance { get; init; }
    
    /// <summary>
    /// Unit of the air-line distance.
    /// </summary>
    public string Unit { get; init; }
}