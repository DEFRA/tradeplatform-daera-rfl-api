// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace DEFRA.Trade.API.DAERA.RFL.V1.Dtos;

/// <summary>
/// The details about who is issuing the update request
/// </summary>
public sealed class Issuer
{
    /// <summary>
    /// The scoped identifer of the isuser
    /// </summary>
    public IdType Id { get; set; }

    /// <summary>
    /// The localized name of the issuer
    /// </summary>
    public TextType Name { get; set; }

    /// <summary>
    /// The role of the issuer
    /// </summary>
    public IdType RoleCode { get; set; }
}