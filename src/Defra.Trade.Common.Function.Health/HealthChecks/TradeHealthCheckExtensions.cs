﻿// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Diagnostics.CodeAnalysis;

namespace Defra.Trade.Common.Function.Health.HealthChecks;

[ExcludeFromCodeCoverage(Justification = "Unable to mock service provider. This can be looked at later stage")]
public static class TradeHealthCheckExtensions
{
    public static IHealthChecksBuilder AddAzureServiceBusCheck(
        this IHealthChecksBuilder builder,
        IConfiguration configuration,
        string serviceBusConnectionConfigPath,
        string queueName)
    {
        string servicesBusConnectionString = configuration.GetValue<string>(serviceBusConnectionConfigPath);
        string servicesBusQueueName = queueName;

        builder.Add(new HealthCheckRegistration(
           $"ServiceBus:{queueName}",
            sp => new ServiceBusQueueHealthCheck(servicesBusConnectionString, servicesBusQueueName),
            failureStatus: default,
            tags: default,
            timeout: default));
        return builder;
    }
}
