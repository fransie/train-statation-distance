namespace TrainStationDistance.Model;

public record DistanceDto
{
    public string From { get;}
    public string To { get; }
    public int Distance { get; }
    public string Unit { get; }
}