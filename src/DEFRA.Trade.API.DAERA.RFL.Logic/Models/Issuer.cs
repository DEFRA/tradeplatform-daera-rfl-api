// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Text.Json.Serialization;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Models;

public sealed class Issuer
{
    [JsonPropertyName("ID")]
    public IdType Id { get; set; }

    [JsonPropertyName("Name")]
    public TextType Name { get; set; }

    [JsonPropertyName("RoleCode")]
    public IdType RoleCode { get; set; }
}