// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Threading;
using Defra.Trade.Common.Audit.Enums;
using DEFRA.Trade.API.DAERA.RFL.Logic.Models;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;

namespace DEFRA.Trade.API.DAERA.RFL.Services.Tests.Services;

public class RflServiceTests
{
    private readonly Mock<IProtectiveMonitoringService> _monitoringService;
    private readonly Mock<IQueueClient<RflUpdateRequest>> _queue;
    private readonly RflService _sut;

    public RflServiceTests()
    {
        _monitoringService = new Mock<IProtectiveMonitoringService>(MockBehavior.Strict);
        _queue = new Mock<IQueueClient<RflUpdateRequest>>(MockBehavior.Strict);

        _sut = new RflService(_queue.Object, _monitoringService.Object);
    }

    [Fact]
    public async Task SetRflEntities_WhenCalled_ShouldLogToMonitoringService()
    {
        // Arrange
        using var cts = new CancellationTokenSource();
        var requestEntity = new RflUpdateRequest();
        _monitoringService.Setup(
            X => X.LogSocEventAsync(
                TradeApiAuditCode.ProductionRflUpdates,
                "Successfully received RFL updates for Production", ""))
            .Returns(Task.CompletedTask)
            .Verifiable();
        _queue.Setup(m => m.SendAsync(requestEntity, cts.Token))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await _sut.SetRflEntities(requestEntity, cts.Token);

        // Assert
        Mock.Verify(_queue, _monitoringService);
    }
}
