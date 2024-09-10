// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Common.Audit.Enums;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Services;

public class ProtectiveMonitoringService(
    ILogger<ProtectiveMonitoringService> logger,
    Defra.Trade.ProtectiveMonitoring.Interfaces.IProtectiveMonitoringService socTradeProtectiveMonitoringService) :
    IProtectiveMonitoringService
{
    private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly Defra.Trade.ProtectiveMonitoring.Interfaces.IProtectiveMonitoringService _socProtectiveMonitoringService = socTradeProtectiveMonitoringService ?? throw new ArgumentNullException(nameof(socTradeProtectiveMonitoringService));

    /// <summary>
    /// Log event to SOC
    /// </summary>
    /// <param name="auditCode">Trade Api audit code</param>
    /// <param name="message">Message to log</param>
    /// <param name="additionalInfo">Any additional information to log</param>
    /// <returns></returns>
    public async Task LogSocEventAsync(TradeApiAuditCode auditCode, string message, string additionalInfo = "")
    {
        try
        {
            // Send event to Soc
            await _socProtectiveMonitoringService
                .LogSocEventAsync(auditCode, message: message, additionalInfo: additionalInfo);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed to send audit event for ProtectiveMonitoring.");
        }
    }
}
