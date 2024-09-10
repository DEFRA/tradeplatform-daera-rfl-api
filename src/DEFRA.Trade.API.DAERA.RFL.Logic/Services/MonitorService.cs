// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using DEFRA.Trade.API.DAERA.RFL.Logic.Models;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Services;

/// <inheritdoc />
public class MonitorService : IMonitorService
{
    /// <inheritdoc />
    public Task<HealthReportResponse> GetReport()
    {

        HealthReportResponse healthReportResponse = new()
        {
            Status = HealthStatus.Healthy
        };

        return Task.FromResult(healthReportResponse);
    }
}