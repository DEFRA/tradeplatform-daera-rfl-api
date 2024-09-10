// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.ProtectiveMonitoring.Mappers;
using AC = Defra.Trade.Common.Audit.Enums.TradeApiAuditCode;
using PMC = Defra.Trade.ProtectiveMonitoring.Models.ProtectiveMonitoringCode;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Mappers.ProtectiveMonitoring;

public class ProtectiveMonitoringCodeMapper : ProtectiveMonitoringMapperBase
{
    public ProtectiveMonitoringCodeMapper()
    {
        Map(AC.ProductionRflUpdates, PMC.BusinessTransactions);
    }
}