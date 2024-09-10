// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Diagnostics.CodeAnalysis;
using Defra.Trade.Common.AppConfig;
using DEFRA.Trade.API.DAERA.RFL.Infrastructure;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services;
using Microsoft.Extensions.Hosting;

namespace DEFRA.Trade.API.DAERA.RFL;

/// <summary>
/// Application program file.
/// </summary>
public static class Program
{
    /// <summary>
    /// Application main class.
    /// </summary>
    /// <param name="args">Args</param>
    [ExcludeFromCodeCoverage(Justification = "Process entry point covered by end-to-end tests.")]
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
                {
                    config.ConfigureTradeAppConfiguration(opt =>
                    {
                        opt.UseKeyVaultSecrets = true;
                        opt.RefreshKeys.Add($"{ExtApiAppConfig.AppConfigSettingsName}:{ExtApiAppConfig.RefreshKey}");
                        opt.Select<QueueOptions>("Queues:EhcoRfl:Update");
                    });
                })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}