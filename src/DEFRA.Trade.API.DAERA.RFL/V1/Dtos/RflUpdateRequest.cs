// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace DEFRA.Trade.API.DAERA.RFL.V1.Dtos;

/// <summary>
/// The root of an RFL operator update request
/// </summary>
public sealed class RflUpdateRequest
{
    /// <summary>
    /// The metadata for this request
    /// </summary>
    public RflExchangeDocument ExchangedDocument { get; set; }

    /// <summary>
    /// A list of operators which are contained within this request
    /// </summary>
    public IEnumerable<RflOperatorUpdate> OperatorResponsibleForConsignment { get; set; }
}