using BusinessLogic.Model;
using BusinessLogic.Service;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using TrainStationDistance.Controllers;
using TrainStationDistance.Mapping;
using TrainStationDistance.Model;

namespace ApiTests;

public class DistanceControllerTests
{
    private readonly IDistanceCalculationService _distanceCalculationService =
        Substitute.For<IDistanceCalculationService>();

    private readonly IDtoMapper _dtoMapper = Substitute.For<IDtoMapper>();

    [Test]
    public void GetDistance_WithValidCodes_ReturnsOk()
    {
        // given
        var sut = CreateSut();

        _distanceCalculationService.CalculateDistance(Arg.Any<string>(), Arg.Any<string>())
            .Returns(new DistanceCalculation());
        _dtoMapper.MapToDto(Arg.Any<DistanceCalculation>()).Returns(
            new DistanceCalculationDto
            {
                From = "FF",
                To = "BLS",
                Distance = 423,
                Unit = "km"
            });

        // when
        var response = sut.GetDistance("FF", "BLS");

        // then
        response.Result.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Test]
    public void GetDistance_WithInvalidCodes_ReturnsNotFound()
    {
        // given
        var sut = CreateSut();

        _distanceCalculationService.CalculateDistance(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();

        // when
        var response = sut.GetDistance("FF", "BLS");

        // then
        response.Result.Should().BeOfType<NotFoundObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    private DistanceController CreateSut()
    {
        return new DistanceController(Substitute.For<ILogger<DistanceController>>(), _distanceCalculationService,
            _dtoMapper);
    }
}