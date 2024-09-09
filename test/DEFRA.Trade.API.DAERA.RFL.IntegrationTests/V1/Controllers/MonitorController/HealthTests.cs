// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Net.Http.Json;
using DEFRA.Trade.API.DAERA.RFL.IntegrationTests.Infrastructure;
using DEFRA.Trade.API.DAERA.RFL.IntegrationTests.V1.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DEFRA.Trade.API.DAERA.RFL.IntegrationTests.V1.Controllers.MonitorController;

public class HealthTests(DaeraRflApplicationFactory<Startup> webApplicationFactory) : IClassFixture<DaeraRflApplicationFactory<Startup>>
{
    private const string DefaultClientIpAddress = "12.34.56.789";

    [Fact]
    public async Task Health_WhenCalled_ShouldReturnHealthy()
    {
        // Arrange
        var client = webApplicationFactory.CreateClient();
        webApplicationFactory.AddApimUserContextHeaders(client, Guid.Empty, DefaultClientIpAddress);

        // Act
        var response = await client.GetAsync("health");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var healthReports = await response.Content.ReadFromJsonAsync<TestHealthReportResponse>();

        healthReports.Should().NotBeNull();
        if (healthReports == null)
        {
            Assert.False(true);
        }

        healthReports.Status.Should().Be(HealthStatus.Healthy);
        foreach (var item in healthReports.Entries)
        {
            item.Status.Should().Be(HealthStatus.Healthy);
        }
    }
}
