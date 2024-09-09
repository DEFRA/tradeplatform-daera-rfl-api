// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Text.Json.Serialization;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Models;

public sealed class HealthReportResponse
{
    [JsonPropertyName("status")]
    public HealthStatus Status { get; set; }

    [JsonPropertyName("entries")]
    public List<HealthCheckResultEntry> Entries { get; set; } = [];

    [JsonPropertyName("totalDurationMs")]
    public int TotalDurationMs { get; set; }
}
