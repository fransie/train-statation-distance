using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrainStationDistance.Model;

namespace TrainStationDistance.Controllers;

[ApiController]
[Route($"api/{API_VERSION}/")]
public class DistanceController : ControllerBase
{
    private readonly ILogger<DistanceController> _logger;
    private const string API_VERSION = "v1";

    public DistanceController(ILogger<DistanceController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("distance/{from}/{to}")]
    public DistanceCalculationDto Get(string from, string to)
    {
        _logger.LogInformation($"Received a GET request to /distance with arguments from: \"{from}\" and to: \"{to}\".");
        var response = new DistanceCalculationDto { From = from, To = to, Distance = 423, Unit = "km"};
        return response;
    }
}