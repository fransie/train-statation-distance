namespace BusinessLogic.Model;

public record TrainStation
{
    public string Name { get; init; }
    public string Ds100Code { get; init; }
    public double Longitude { get; init; }
    public double Latitude { get; init; }
}