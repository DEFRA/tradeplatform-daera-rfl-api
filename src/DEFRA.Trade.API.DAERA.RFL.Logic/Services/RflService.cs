// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Common.Audit.Enums;
using DEFRA.Trade.API.DAERA.RFL.Logic.Models;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Services;

public sealed class RflService : IRflService
{
    private readonly IQueueClient<RflUpdateRequest> _client;
    private readonly IProtectiveMonitoringService _monitoringService;

    public RflService(IQueueClient<RflUpdateRequest> client, IProtectiveMonitoringService monitoringService)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(monitoringService);
        _client = client;
        _monitoringService = monitoringService;
    }

    public async Task SetRflEntities(RflUpdateRequest request, CancellationToken cancellationToken)
    {
        await _monitoringService.LogSocEventAsync(
            TradeApiAuditCode.ProductionRflUpdates,
            "Successfully received RFL updates for Production");

        await _client.SendAsync(request, cancellationToken);
    }
}
