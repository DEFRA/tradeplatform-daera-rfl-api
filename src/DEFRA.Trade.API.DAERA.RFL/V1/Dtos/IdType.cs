// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace DEFRA.Trade.API.DAERA.RFL.V1.Dtos;

/// <summary>
/// An identifier with some scope to its uniqueness
/// </summary>
public sealed class IdType : TextContainer
{
    /// <summary>
    /// The list agency Id
    /// </summary>
    public string ListAgencyId { get; set; }

    /// <summary>
    /// The scheme agency Id
    /// </summary>
    public string SchemeAgencyId { get; set; }

    /// <summary>
    /// The scheme Id
    /// </summary>
    public string SchemeId { get; set; }

    /// <summary>
    /// The scheme name
    /// </summary>
    public string SchemeName { get; set; }
}