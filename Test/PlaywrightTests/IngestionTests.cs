using System.Net;
using FluentAssertions;

public class IngestionTests : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client;

    public IngestionTests(ApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ScrapeEndpoint_ShouldReturnSuccess()
    {
        // Act
        var response = await _client.PostAsync("/ingestion/scrape", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}