// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;

public interface IQueueClient<in T> : IAsyncDisposable
{
    Task SendAsync(T message, CancellationToken cancellationToken = default);
}
