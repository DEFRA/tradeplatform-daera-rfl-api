﻿// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Azure.Core;
using Azure.Messaging.ServiceBus;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;

public interface IServiceBusClientFactory
{
    ServiceBusClient Create(string connectionString, ServiceBusClientOptions options = default);

    ServiceBusClient Create(string fullyQualifiedNamespace, TokenCredential credential, ServiceBusClientOptions options = default);
}