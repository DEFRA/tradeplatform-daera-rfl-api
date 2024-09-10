// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Common.Audit.Enums;
using Defra.Trade.ProtectiveMonitoring.Models;
using DEFRA.Trade.API.DAERA.RFL.Logic.Mappers.ProtectiveMonitoring;

namespace DEFRA.Trade.API.DAERA.RFL.Services.Tests.Mappers.ProtectiveMonitoring;

public class ProtectiveMonitoringCodeMapperTests
{
    [Fact]
    public void ProtectiveMonitoringCodeMapper_Should_MapAsExpected()
    {
        // Arrange
        var mapper = new ProtectiveMonitoringCodeMapper();

        // Act
        var result = mapper.Map(TradeApiAuditCode.ProductionRflUpdates);

        // Assert
        result.Code.Should().BeEquivalentTo(ProtectiveMonitoringCode.BusinessTransactions);
    }
}