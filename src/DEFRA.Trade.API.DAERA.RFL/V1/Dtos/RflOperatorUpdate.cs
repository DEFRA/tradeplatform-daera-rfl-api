// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace DEFRA.Trade.API.DAERA.RFL.V1.Dtos;

/// <summary>
/// The details of the operator being updated
/// </summary>
public sealed class RflOperatorUpdate
{
    /// <summary>
    /// The operators classification code
    /// </summary>
    public IdType ClassificationCode { get; set; }

    /// <summary>
    /// The scoped id of the operator
    /// </summary>
    public IdType Id { get; set; }

    /// <summary>
    /// The localized name of the operator
    /// </summary>
    public TextType Name { get; set; }

    /// <summary>
    /// The address of the operator
    /// </summary>
    public RflOperatorAddressUpdate PostalAddress { get; set; }
}