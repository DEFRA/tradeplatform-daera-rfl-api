// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Text.Json.Serialization;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Models;

public sealed class IdType : TextContainer
{
    [JsonPropertyName("listAgencyID")]
    public string ListAgencyId { get; set; }

    [JsonPropertyName("schemeAgencyID")]
    public string SchemeAgencyId { get; set; }

    [JsonPropertyName("schemeID")]
    public string SchemeId { get; set; }

    [JsonPropertyName("schemeName")]
    public string SchemeName { get; set; }
}