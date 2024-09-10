// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace DEFRA.Trade.API.DAERA.RFL.V1.Dtos;

/// <summary>
/// A set of headers describing meta-information about the request
/// </summary>
public sealed class RflExchangeDocument
{
    /// <summary>
    /// The id of the request
    /// </summary>
    public IdType Id { get; set; }

    /// <summary>
    /// The timestamp at which the request was sent
    /// </summary>
    public FormattedDateTime IssueDateTime { get; set; }

    /// <summary>
    /// Who issued the request
    /// </summary>
    public Issuer Issuer { get; set; }

    /// <summary>
    /// The type of the request
    /// </summary>
    public IdType TypeCode { get; set; }
}