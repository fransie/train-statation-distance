using BusinessLogic.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrainStationDistance.Model;

namespace TrainStationDistance.Controllers;

[ApiController]
[Route($"api/{ApiVersion}/")]
public class DistanceController : ControllerBase
{
    private readonly IDistanceCalculationService _calculationService;
    private readonly ILogger<DistanceController> _logger;
    private readonly DtoMapper _dtoMapper;
    private const string ApiVersion = "v1";

    public DistanceController(ILogger<DistanceController> logger, IDistanceCalculationService calculationService, DtoMapper dtoMapper)
    {
        _logger = logger;
        _calculationService = calculationService;
        _dtoMapper = dtoMapper;
    }

    [HttpGet]
    [Route("distance/{from}/{to}")]
    public DistanceCalculationDto Get(string from, string to)
    {
        _logger.LogInformation($"Received a GET request to /distance with arguments from: \"{from}\" and to: \"{to}\".");
        var response = _calculationService.CalculateDistance(from, to);
        return _dtoMapper.MapToDto(response);
    }
}