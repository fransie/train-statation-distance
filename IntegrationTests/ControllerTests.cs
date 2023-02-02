using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using TrainStationDistance;
using TrainStationDistance.Model;

namespace IntegrationTests;

public class ControllerTests
{
    private readonly HttpClient _client = new WebApplicationFactory<Startup>().CreateClient();

    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    [TestCase("FF", "BLS", "Frankfurt(Main)Hbf", "Berlin Hbf", 423)]
    [TestCase("BLS", "FF", "Berlin Hbf", "Frankfurt(Main)Hbf", 423)]
    [TestCase("KK", "KB", "Köln Hbf", "Bonn Hbf", 25)]
    public async Task GetDistance_ReturnsCorrectDistance(string from, string to, string fromName, string toName,
        int expectedDistance)
    {
        // given
        const string expectedUnit = "km";

        // when
        var response = await _client.GetAsync($"/api/v1/distance/{from}/{to}");

        // then
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var distanceDto =
            JsonSerializer.Deserialize<DistanceCalculationDto>(await response.Content.ReadAsStringAsync(), _options);
        distanceDto.Should().NotBeNull();
        distanceDto!.From.Should().Be(fromName);
        distanceDto.To.Should().Be(toName);
        distanceDto.Distance.Should().Be(expectedDistance);
        distanceDto.Unit.Should().Be(expectedUnit);
    }

    [Test]
    public async Task GetDistance_WithInvalidCode_ReturnsNotFound()
    {
        // given
        const string invalidCode = "invalid";

        // when
        var response = await _client.GetAsync($"/api/v1/distance/{invalidCode}/{invalidCode}");

        // then
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}