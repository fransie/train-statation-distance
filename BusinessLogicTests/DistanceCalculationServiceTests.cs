using BusinessLogic.Model;
using BusinessLogic.Service;
using FluentAssertions;
using NUnit.Framework;

namespace BusinessLogicTests;

public class TrainStationServiceTests
{
    [TestCase("FF", "Frankfurt(Main)Hbf", "BLS","Berlin Hbf", 423)]
    [TestCase("ff", "Frankfurt(Main)Hbf", "bls","Berlin Hbf", 423)]
    [TestCase("KK", "Köln Hbf", "KB", "Bonn Hbf", 25)]
    public void CalculateDistance_WithValidCodes_ReturnsCorrectDistance(string from, string fromName, string to, string toName, int expectedDistance)
    {
        // given
        var sut = CreateSut();
        var expectedDistanceCalculation = new DistanceCalculation
        {
            From = fromName,
            To = toName,
            Distance = expectedDistance,
            Unit = "km"
        };
        
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
        const string invalidCode = "invalid";
        
        // when
        var distanceCalculation = sut.CalculateDistance(invalidCode, invalidCode);
        
        // then
        distanceCalculation.Should().BeNull();
    }

    
    private IDistanceCalculationService CreateSut()
    {
        throw new NotImplementedException();
    }
}