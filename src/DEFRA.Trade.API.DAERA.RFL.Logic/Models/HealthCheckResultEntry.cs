// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Models;

public sealed class HealthCheckResultEntry
{
    public string Key { get; set; }
    public string Description { get; set; }
    public string ExceptionMessage { get; set; }
    public int DurationMs { get; set; }
    public HealthStatus Status { get; set; }
    public List<string> Tags { get; set; }

    public HealthCheckResultEntry()
    {
        Tags = [];
    }

    public HealthCheckResultEntry(string key)
    {
        Tags = [];
        Key = key;
    }
}
