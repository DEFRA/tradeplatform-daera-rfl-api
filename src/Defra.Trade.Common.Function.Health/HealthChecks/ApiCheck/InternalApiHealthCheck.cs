// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Diagnostics.CodeAnalysis;

namespace Defra.Trade.Common.Function.Health.HealthChecks.ApiCheck;

/// <summary>
/// Internal API.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Health check extensions cant be unit tested")]
public abstract class InternalApiHealthCheck : IHealthCheck
{
    public abstract Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default);

    protected async Task<HealthCheckResult> ExecuteCheckAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        HealthCheckResult result;
        try
        {
            result = await CheckHealthInternalAsync(context, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            result = HealthCheckResult.Unhealthy("The health check operation timed out");
        }
        catch (Exception ex)
        {
            result = HealthCheckResult.Unhealthy($"Exception during check: {ex.GetType().FullName}", ex);
        }

        return result;
    }

    protected abstract Task<HealthCheckResult> CheckHealthInternalAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default);
}
