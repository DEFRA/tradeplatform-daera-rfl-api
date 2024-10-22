// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using DEFRA.Trade.API.DAERA.RFL.Logic.Models;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;

/// <summary>
/// Monitor service
/// </summary>
public interface IMonitorService
{
    /// <summary>
    /// Get a health report containing database context information
    /// </summary>
    /// <returns>The health report</returns>
    public Task<HealthReportResponse> GetReport();
}