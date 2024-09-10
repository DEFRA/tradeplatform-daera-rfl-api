// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace DEFRA.Trade.API.DAERA.RFL.V1.Dtos;

public sealed class TextType : TextContainer
{
    /// <summary>
    /// The ISO language code of the content
    /// </summary>
    public string LanguageId { get; set; }
}