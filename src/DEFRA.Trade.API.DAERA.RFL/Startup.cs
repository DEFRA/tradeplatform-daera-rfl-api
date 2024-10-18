// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Common.Api.Infrastructure;
using Defra.Trade.Common.ExternalApi.ApimIdentity;
using Defra.Trade.Common.ExternalApi.Auditing;
using Defra.Trade.Common.Sql.Infrastructure;
using DEFRA.Trade.API.DAERA.RFL.Infrastructure;
using DEFRA.Trade.API.DAERA.RFL.Logic.Extensions;

namespace DEFRA.Trade.API.DAERA.RFL;

/// <summary>
/// Startup class.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Startup"/> class.
/// </remarks>
/// <param name="configuration">Application Config.</param>
public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;

    /// <summary>
    /// Method to configure application startup.
    /// </summary>
    /// <param name="app">Application builder.</param>
    /// <param name="env">Web environment</param>
    /// <param name="logger">Application logger</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        logger.LogStartup(
            env.EnvironmentName,
            env.ApplicationName,
            env.ContentRootPath);

        app.UseTradeExternalAuditing();
        app.UseTradeApp(env);
    }

    /// <summary>
    /// Config services registrations.
    /// </summary>
    /// <param name="services">Application Service collection.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTradeApi(Configuration);
        services.AddTradeExternalApimIdentity(Configuration);
        services.AddTradeExternalAuditing(Configuration);
        services.AddTradeSql(Configuration);
        services.AddServiceRegistrations(Configuration);
    }
}
