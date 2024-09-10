// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Azure.Core;
using Azure.Messaging.ServiceBus;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Services;

public class ServiceBusClientFactory : IServiceBusClientFactory
{
    public ServiceBusClient Create(string connectionString, ServiceBusClientOptions options = null)
    {
        return new(connectionString, options);
    }

    public ServiceBusClient Create(string fullyQualifiedNamespace, TokenCredential credential, ServiceBusClientOptions options = null)
    {
        return new(fullyQualifiedNamespace, credential, options);
    }
}