using BusinessLogic.Model;
using FluentAssertions;
using NUnit.Framework;
using TrainStationDistance.Mapping;

namespace ApiTests;

public class DtoMapperTests
{
    [Test]
    public void MapToDto_ProducesCorrectMapping()
    {
        // given
        var sut = CreateSut();

        const string kölnHbf = "Köln Hbf";
        const string bonnHbf = "Bonn Hbf";
        const int distance = 25;
        const string unit = "km";
        var bo = new DistanceCalculation
        {
            From = kölnHbf,
            To = bonnHbf,
            Distance = distance,
            Unit = unit
        };

        // when
        var dto = sut.MapToDto(bo);

        // then
        dto.From.Should().Be(kölnHbf);
        dto.To.Should().Be(bonnHbf);
        dto.Distance.Should().Be(distance);
        dto.Unit.Should().Be(unit);
    }

    private static IDtoMapper CreateSut()
    {
        return new DtoMapper();
    }
}