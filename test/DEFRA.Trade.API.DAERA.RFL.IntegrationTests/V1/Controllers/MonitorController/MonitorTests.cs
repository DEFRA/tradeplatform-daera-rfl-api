// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Net.Http.Json;
using DEFRA.Trade.API.DAERA.RFL.IntegrationTests.Infrastructure;
using DEFRA.Trade.API.DAERA.RFL.IntegrationTests.V1.Models;
using DEFRA.Trade.API.DAERA.RFL.Logic.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DEFRA.Trade.API.DAERA.RFL.IntegrationTests.V1.Controllers.MonitorController;

public class MonitorTests(DaeraRflApplicationFactory<Startup> webApplicationFactory) : IClassFixture<DaeraRflApplicationFactory<Startup>>
{
    private readonly string _defaultClientIpAddress = "12.34.56.789";
    private readonly DaeraRflApplicationFactory<Startup> _webApplicationFactory = webApplicationFactory ?? throw new ArgumentNullException(nameof(webApplicationFactory));

    [Fact]
    public async Task Monitor_WhenCalled_ShouldReturnHealthy()
    {
        // Arrange
        var client = _webApplicationFactory.CreateClient();
        _webApplicationFactory.AddApimUserContextHeaders(client, Guid.Empty, _defaultClientIpAddress);

        var monitorResponse = new HealthReportResponse()
        {
            Status = HealthStatus.Healthy,
            TotalDurationMs = 4
        };

        _webApplicationFactory.MonitorService
            .Setup(x => x.GetReport())
            .ReturnsAsync(monitorResponse);

        // Act
        var response = await client.GetAsync("monitor");

        // Assert
        var content = await response.Content.ReadFromJsonAsync<TestHealthReportResponse>();
        content.Should().NotBeNull();
        content.Status.Should().Be(HealthStatus.Healthy);
    }

    [Fact]
    public async Task Monitor_WhenCalled_ShouldReturnUnHealthyWith500()
    {
        // Arrange
        var client = _webApplicationFactory.CreateClient();
        _webApplicationFactory.AddApimUserContextHeaders(client, Guid.Empty, _defaultClientIpAddress);

        var monitorResponse = new HealthReportResponse()
        {
            Status = HealthStatus.Unhealthy,
            Entries =
                [
                    new()
                    {
                        Key = "first",
                        Status = HealthStatus.Unhealthy,
                        Description = "description",
                        DurationMs = 3,
                        ExceptionMessage = new ArithmeticException().Message
                    }
                ],
            TotalDurationMs = 4
        };

        _webApplicationFactory.MonitorService
            .Setup(x => x.GetReport())
            .ReturnsAsync(monitorResponse);

        // Act
        var response = await client.GetAsync("monitor");

        // Assert
        var content = await response.Content.ReadFromJsonAsync<TestHealthReportResponse>();
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        content.Should().NotBeNull();
        content.Status.Should().Be(HealthStatus.Unhealthy);
        content.Entries.Count.Should().Be(1);
    }

    [Fact]
    public async Task Monitor_WhenCalled_ShouldReturnNullWithProblem()
    {
        // Arrange
        var client = _webApplicationFactory.CreateClient();
        _webApplicationFactory.AddApimUserContextHeaders(client, Guid.Empty, _defaultClientIpAddress);



        _webApplicationFactory.MonitorService
            .Setup(x => x.GetReport())
            .ReturnsAsync((HealthReportResponse)null);

        // Act
        var response = await client.GetAsync("monitor");

        // Assert
        var content = await response.Content.ReadFromJsonAsync<TestHealthReportResponse>();
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        content.Entries.Should().BeEmpty();
    }
}
