// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Services;

/// <inheritdoc cref="IDateTimeProvider"/>
public class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc/>
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}