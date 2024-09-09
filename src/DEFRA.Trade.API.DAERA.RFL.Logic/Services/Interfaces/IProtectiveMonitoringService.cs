// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Common.Audit.Enums;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;

public interface IProtectiveMonitoringService
{
    Task LogSocEventAsync(TradeApiAuditCode auditCode, string message, string additionalInfo = "");
}