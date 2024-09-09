// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DEFRA.Trade.API.DAERA.RFL.IntegrationTests.V1.Models;

internal sealed class TestHealthReportResponse
{
    [JsonPropertyName("status")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HealthStatus Status { get; set; }

    [JsonPropertyName("entries")]
    public List<Entry> Entries { get; set; } = [];

    [JsonPropertyName("totalDurationMs")]
    public int TotalDurationMs { get; set; }
}

public sealed class Entry
{
    public string Key { get; set; }
    public string Description { get; set; }
    public string ExceptionMessage { get; set; }
    public int DurationMs { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HealthStatus Status { get; set; }

    public List<string> Tags { get; set; } = [];
}
