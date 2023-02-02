using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using TrainStationDistance;
using TrainStationDistance.Model;

namespace IntegrationTests
{
    public class ControllerTests
    {
        private WebApplicationFactory<Startup> _application;
        private HttpClient _client;
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase   
        };

        [OneTimeSetUp]
        public void Setup()
        {
            _application = new WebApplicationFactory<Startup>();
            _client = _application.CreateClient();

        }

        [TestCase("FF", "BLS", 423)]
        [TestCase("BLS", "FF", 423)]
        [TestCase("KK", "KB", 25)]
        public async Task GetDistance_ReturnsCorrectDistance(string from, string to, int expectedDistance)
        {
            // given
            const string expectedUnit = "km";
            
            // when
            var response = await _client.GetAsync($"/api/v1/distance/{from}/{to}");

            // then
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var test = await response.Content.ReadAsStringAsync();
            var distanceDto = JsonSerializer.Deserialize<DistanceDto>(await response.Content.ReadAsStringAsync(), _options);
            distanceDto.From.Should().Be(from);
            distanceDto.To.Should().Be(to);
            distanceDto.Distance.Should().Be(expectedDistance);
            distanceDto.Unit.Should().Be(expectedUnit);
        }
        
        [Test]
        public async Task GetDistance_WithUnknownCode_ReturnsNotFound()
        {
            // given
            const string unknownTrainStation = "HALLO";
            
            // when
            var response = await _client.GetAsync($"/api/v1/distance/{unknownTrainStation}/{unknownTrainStation}");

            // then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}