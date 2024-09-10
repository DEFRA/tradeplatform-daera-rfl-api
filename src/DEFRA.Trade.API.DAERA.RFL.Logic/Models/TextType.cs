// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Text.Json.Serialization;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Models;

public sealed class TextType : TextContainer
{
    [JsonPropertyName("languageID")]
    public string LanguageId { get; set; }
}