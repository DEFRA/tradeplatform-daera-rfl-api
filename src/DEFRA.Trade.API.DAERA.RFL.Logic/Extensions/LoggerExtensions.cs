// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Microsoft.Extensions.Logging;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Extensions;

public static partial class LoggerExtensions
{
    [LoggerMessage(
    EventId = 0,
    EventName = nameof(LogStartup),
    Level = LogLevel.Information,
    Message = "Starting {EnvironmentName} {ApplicationName} from {ContentRootPath}")]
    public static partial void LogStartup(this ILogger logger, string environmentName, string applicationName, string contentRootPath);

    [LoggerMessage(
    EventId = 1,
    EventName = nameof(RflUpdateRequestStart),
    Level = LogLevel.Information,
    Message = "Received update request for document id : {RequestDocId}")]
    public static partial void RflUpdateRequestStart(this ILogger logger, string requestDocId);

    [LoggerMessage(
    EventId = 2,
    EventName = nameof(RflUpdateRequestSuccess),
    Level = LogLevel.Information,
    Message = "Completed update request for document id : {RequestDocId}")]
    public static partial void RflUpdateRequestSuccess(this ILogger logger, string requestDocId);
}
