namespace BusinessLogic.Model;

public class DistanceCalculation
{
    public string From { get; init; } = string.Empty;
    public string To { get; init; } = string.Empty;
    public int Distance { get; init; }
    public string Unit { get; init; } = string.Empty;
}