// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Text.Json.Serialization;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Models;

public sealed class RflExchangeDocument
{
    [JsonPropertyName("ID")]
    public IdType Id { get; set; }

    [JsonPropertyName("IssueDateTime")]
    public FormattedDateTime IssueDateTime { get; set; }

    [JsonPropertyName("Issuer")]
    public Issuer Issuer { get; set; }

    [JsonPropertyName("TypeCode")]
    public IdType TypeCode { get; set; }
}