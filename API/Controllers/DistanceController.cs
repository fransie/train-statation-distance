using BusinessLogic.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrainStationDistance.Model;

namespace TrainStationDistance.Controllers;

[ApiController]
[Route($"api/{ApiVersion}/")]
public class DistanceController : ControllerBase
{
    private const string ApiVersion = "v1";
    private readonly IDistanceCalculationService _calculationService;
    private readonly DtoMapper _dtoMapper;
    private readonly ILogger<DistanceController> _logger;

    public DistanceController(ILogger<DistanceController> logger, IDistanceCalculationService calculationService,
        DtoMapper dtoMapper)
    {
        _logger = logger;
        _calculationService = calculationService;
        _dtoMapper = dtoMapper;
    }

    /// <summary>
    ///     Calculates the air-line distance between two German intercity train stations.
    /// </summary>
    /// <param name="from">DS100Code of start train station, e.g. FF.</param>
    /// <param name="to">DS100Code of end train station, e.g. BLS.</param>
    /// <returns>Distance calculation.</returns>
    /// <response code="200">Returns the distance.</response>
    /// <response code="404">Indicates that one of the DC100Codes is invalid.</response>
    [HttpGet("distance/{from}/{to}", Name = "GetDistance")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DistanceCalculationDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DistanceCalculationDto> GetDistance(string from, string to)
    {
        _logger.LogInformation("Received a GET request to /distance/{from}/{to}.", from, to);
        var distanceCalculation = _calculationService.CalculateDistance(from, to);
        if (distanceCalculation is null)
        {
            return NotFound(new ProblemDetails { Detail = "One or both of the DS100Codes are invalid." });
        }

        return Ok(_dtoMapper.MapToDto(distanceCalculation));
    }
}