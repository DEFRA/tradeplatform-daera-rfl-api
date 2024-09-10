// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using DEFRA.Trade.API.DAERA.RFL.Logic.Models;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DEFRA.Trade.API.DAERA.RFL.V1.Controllers;

/// <summary>
/// Monitor controller.
/// </summary>
/// <remarks>
/// Object creator for the type HealthController.
/// </remarks>
/// <exception cref="ArgumentNullException"></exception>
[ApiVersion("1")]
[Route("/monitor")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class MonitorController(IMonitorService monitorService) : ControllerBase
{
    private readonly IMonitorService _monitorService = monitorService ?? throw new ArgumentNullException(nameof(monitorService));

    /// <summary>
    /// Application monitoring service to look for downstream applications health status.
    /// </summary>
    /// <returns>Health report response</returns>
    [HttpGet()]
    [ProducesResponseType(typeof(HealthReportResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HealthReportResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Index()
    {
        var report = await _monitorService.GetReport();

        if (report is null)
        {
            return Problem();
        }

        return report.Status.Equals(HealthStatus.Healthy) ? Ok(report) : StatusCode(StatusCodes.Status500InternalServerError, report);
    }
}
