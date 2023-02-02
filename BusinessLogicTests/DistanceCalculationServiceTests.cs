using BusinessLogic.Model;
using BusinessLogic.Service;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace BusinessLogicTests;

public class DistanceCalculationServiceTests
{
    private readonly ITrainStationRepository _trainStationRepository = Substitute.For<ITrainStationRepository>();

    [TestCase("FF", "BLS", 423)]
    [TestCase("ff", "bls", 423)]
    public void CalculateDistance_WithValidCodes_ReturnsCorrectDistance(string from, string to, int expectedDistance)
    {
        // given
        var sut = CreateSut();
        const string frankfurtHbf = "Frankfurt(Main)Hbf";
        const string berlinHbf = "Berlin Hbf";
        var expectedDistanceCalculation = new DistanceCalculation
        {
            From = "Frankfurt(Main)Hbf",
            To = berlinHbf,
            Distance = expectedDistance,
            Unit = "km"
        };
        _trainStationRepository.GetByDs100Code(from).Returns(
            new TrainStation
            {
                Ds100Code = from,
                Name = frankfurtHbf,
                Longitude = 8.663789,
                Latitude = 50.107145
            });
        _trainStationRepository.GetByDs100Code(to).Returns(
            new TrainStation
            {
                Ds100Code = from,
                Name = berlinHbf,
                Longitude = 13.369545,
                Latitude = 52.525592
            });

        // when
        var distanceCalculation = sut.CalculateDistance(from, to);

        // then
        distanceCalculation.Should().BeEquivalentTo(expectedDistanceCalculation);
    }

    [Test]
    public void CalculateDistance_WithInvalidCodes_ReturnsNull()
    {
        // given
        var sut = CreateSut();
        const string invalidCode = "INVALID";
        _trainStationRepository.GetByDs100Code(invalidCode).Throws(new ArgumentException());

        // when
        var distanceCalculation = sut.CalculateDistance(invalidCode, invalidCode);

        // then
        distanceCalculation.Should().BeNull();
    }


    private IDistanceCalculationService CreateSut()
    {
        return new DistanceCalculationService(_trainStationRepository, Substitute.For<ILogger<IDistanceCalculationService>>());
    }
}