// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace DEFRA.Trade.API.DAERA.RFL.V1.Dtos;

/// <summary>
/// A date and time paired with a format code
/// </summary>
public sealed class FormattedDateTime : TextContainer
{
    /// <summary>
    /// The format of the date time provided.
    /// </summary>
    public string Format { get; set; }
}