// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Text.Json.Serialization;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Models;

public abstract class TextContainer
{
    [JsonPropertyName("content")]
    public string Content { get; set; }
}