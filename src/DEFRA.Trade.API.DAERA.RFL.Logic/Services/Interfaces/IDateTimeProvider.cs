// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;

/// <summary>Retrieve DateTime information.</summary>
public interface IDateTimeProvider
{
    /// <summary>Get the DateTime as it is now.</summary>
    DateTimeOffset Now { get; }
}