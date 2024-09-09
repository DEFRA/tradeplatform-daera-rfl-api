// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Azure.Messaging.ServiceBus;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Services;

public abstract class ServiceBusQueueClient<T> : IQueueClient<T>
{
    private readonly ServiceBusSender _sender;

    protected ServiceBusQueueClient(IServiceBusClientFactory factory, QueueOptions options)
    {
        ArgumentNullException.ThrowIfNull(factory);
        ArgumentNullException.ThrowIfNull(options);
        var client = factory.Create(options.ConnectionString);
        _sender = client.CreateSender(options.QueueName);
    }

    public async ValueTask DisposeAsync()
    {
        await _sender.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public async Task SendAsync(T message, CancellationToken cancellationToken = default)
    {
        await _sender.SendMessageAsync(CreateMessage(message), cancellationToken);
    }

    protected abstract ServiceBusMessage CreateMessage(T message);
}
