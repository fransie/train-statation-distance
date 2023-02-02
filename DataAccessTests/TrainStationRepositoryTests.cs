using DataAccess;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace DataAccessTests;

public class TrainStationRepositoryTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task GetAllAsync_ReturnsAllTrainStations()
    {
        // given
        var sut = CreateSut();
        const int expectedTrainStations = 358;

        // when
        var trainsStations = await sut.GetAllAsync();
        
        // then
        trainsStations.Count.Should().Be(expectedTrainStations);
    }

    [Test]
    public async Task GetByDs100CodeAsync_WithValidCode_ReturnsCorrectTrainStation()
    {
        // given
        var sut = CreateSut();
        const string dsCode = "FF";
        const double expectedLongitude = 8.663789;
        const double expectedLatitude = 50.107145;

        // when
        var trainsStation = await sut.GetByDs100CodeAsync(dsCode);
        
        // then
        trainsStation.Ds100Code.Should().Be("FF");
        trainsStation.Name.Should().Be("Frankfurt(Main)Hbf");
        trainsStation.Longitude.Should().Be(expectedLongitude);
        trainsStation.Latitude.Should().Be(expectedLatitude);
    }

    [Test]
    public async Task GetByDs100CodeAsync_WithInvalidCode_ThrowsArgumentException()
    {
        // given
        var sut = CreateSut();
        const string invalidDsCode = "invalid";

        // when
        var trainsStation = () => sut.GetByDs100CodeAsync(invalidDsCode);
        
        // then
        await trainsStation.Should().ThrowAsync<ArgumentException>();
    }

    
    private ITrainStationRepository CreateSut()
    {
        return new TrainStationRepository(Substitute.For<ILogger<TrainStationRepository>>());
    }
}