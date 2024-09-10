// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Common.Function.Health.HealthChecks;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;
using FluentValidation.AspNetCore;
using Models = DEFRA.Trade.API.DAERA.RFL.Logic.Models;

namespace DEFRA.Trade.API.DAERA.RFL.Infrastructure;

/// <summary>
/// Service registration class.
/// </summary>
public static class ServiceRegistrations
{
    /// <summary>
    /// Extension method for service registrations.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Application configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddServiceRegistrations(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddAutoMapper(typeof(Startup))
            .AddValidatorsFromAssemblyContaining<Startup>(lifetime: ServiceLifetime.Transient)
            .AddFluentValidationAutoValidation()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly))
            .AddV1Registrations(configuration)
            .AddLogicRegistrations()
            .AddApiOptions(configuration)
            .AddSwaggerExamples()
            .AddProtectiveMonitoring(configuration)
            .AddDaeraCertificatesHealthChecks(configuration);
    }

    private static IServiceCollection AddApiOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ApimExternalApisSettings>()
            .Bind(configuration.GetSection(ApimExternalApisSettings.OptionsName));
        return services;
    }

    private static IServiceCollection AddDaeraCertificatesHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddCheck<AppSettingHealthCheck>("Queues:EhcoRfl:Update:ConnectionString")
            .AddCheck<AppSettingHealthCheck>("DaeraRflApiSettings:BaseUrl")
            .AddCheck<AppSettingHealthCheck>("ProtectiveMonitoringSettings:Environment")
            .AddCheck<AppSettingHealthCheck>("DaeraRflApiSettings:DaeraRflApiPathV1")
            .AddAzureServiceBusCheck(configuration, "Queues:EhcoRfl:Update:ConnectionString", EhcoRflUpdateQueueClient.OptionsName);

        services
            .AddScoped<IMonitorService, MonitorService>();

        return services;
    }

    private static IServiceCollection AddLogicRegistrations(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeParser, DateTimeParser>();
        services.AddSingleton<IServiceBusClientFactory, ServiceBusClientFactory>();
        return services;
    }

    private static IServiceCollection AddProtectiveMonitoring(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddProtectiveMonitoring(
            configuration,
            options => configuration.Bind(ProtectiveMonitoringSettings.OptionsName, options));
        return services;
    }

    private static IServiceCollection AddSwaggerExamples(this IServiceCollection services)
    {
        return services;
    }

    private static IServiceCollection AddV1Registrations(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddScoped<IDateTimeProvider, DateTimeProvider>()
            .AddScoped<IProtectiveMonitoringService, ProtectiveMonitoringService>()
            .AddScoped<IRflService, RflService>()
            .AddScoped<IQueueClient<Models.RflUpdateRequest>, EhcoRflUpdateQueueClient>()
            .Configure<QueueOptions>(EhcoRflUpdateQueueClient.OptionsName, configuration.GetSection("Queues:EhcoRfl:Update"));
    }
}
