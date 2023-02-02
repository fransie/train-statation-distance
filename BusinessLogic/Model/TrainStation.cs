namespace BusinessLogic.Model;

public record TrainStation
{
    public string Name { get; init; } = string.Empty;
    public string Ds100Code { get; init; } = string.Empty;
    public double Longitude { get; init; }
    public double Latitude { get; init; }
}