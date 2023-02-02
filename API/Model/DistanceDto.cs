namespace TrainStationDistance.Model;

public record DistanceDto
{
    public string From { get; init; }
    public string To { get; init; }
    public int Distance { get; init; }
    public string Unit { get; init; }
}