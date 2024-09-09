// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using DEFRA.Trade.API.DAERA.RFL.Logic.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DEFRA.Trade.API.DAERA.RFL.Services.Tests.Services;

public class MonitorServiceTests
{

    private readonly MonitorService _sut = new();

    [Fact]
    public async Task GetReport_ShouldNotBelNull()
    {
        // Arrange

        // Act
        var result = await _sut.GetReport();

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(HealthStatus.Healthy);
        result.TotalDurationMs.Should().BeGreaterThanOrEqualTo(0);
        result.Entries.Should().HaveCount(0);
    }
}
