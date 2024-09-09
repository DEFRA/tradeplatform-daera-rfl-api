// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Common.ExternalApi.Auditing.Data;
using DEFRA.Trade.API.DAERA.RFL.Logic.Models;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DEFRA.Trade.API.DAERA.RFL.IntegrationTests.Infrastructure;

public class DaeraRflApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    public Mock<IAuditRepository> AuditRepository { get; set; }

    public Mock<IDateTimeProvider> DateTimeProvider { get; set; }

    public Mock<IQueueClient<RflUpdateRequest>> EhcoRflClient { get; }

    public Mock<IMonitorService> MonitorService { get; set; }

    private string ApiVersion { get; set; } = "1";

    public DaeraRflApplicationFactory() : base()
    {
        ClientOptions.AllowAutoRedirect = false;
        DateTimeProvider = new();
        MonitorService = new();
        AuditRepository = new();
        EhcoRflClient = new();
    }

    public void AddApimUserContextHeaders(HttpClient client, Guid? clientId, string clientIpAddress)
    {
        if (clientId.HasValue)
        {
            client.DefaultRequestHeaders.Add("x-client-id", clientId.Value.ToString());
        }

        if (clientIpAddress != null)
        {
            client.DefaultRequestHeaders.Add("x-client-ipaddress", clientIpAddress);
        }
    }

    protected override void ConfigureClient(HttpClient client)
    {
        base.ConfigureClient(client);

        client.BaseAddress = new Uri("https://localhost:5001");
        client.DefaultRequestHeaders.Add("x-api-version", ApiVersion);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.UseEnvironment(Environments.Production);

        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            configBuilder.AddInMemoryCollection(
                new Dictionary<string, string>
                {
                    ["https_port"] = "",
                    ["OpenApi:UseXmlComments"] = "false",
                    ["CommonError:ExposeErrorDetail"] = "true",
                    ["DaeraRflApiSettings:BaseUrl"] = "https://integrationtest-gateway.trade.azure.defra.cloud",
                    ["DaeraRflApiSettings:DaeraRflApiPathV1"] = "/daera-rfl/v1",
                    ["DaeraRflApiSettings:TestApiUri"] = "This is test string from North Europe",
                    ["SocSettings:EventHubName"] = "insights-application-logs",
                    ["SocSettings:EventHubConnectionString"] = "Endpoint=sb://not.a.real.service.bus/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=not-a-real-key",
                    ["Queues:EhcoRfl:Update:ConnectionString"] = "Endpoint=sb://not.a.real.service.bus/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=not-a-real-key",
                    ["ServiceBus:ConnectionString"] = "Endpoint=sb://not.a.real.service.bus/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=not-a-real-key",
                    ["ProtectiveMonitoringSettings:Enabled"] = bool.FalseString,
                    ["ProtectiveMonitoringSettings:Environment"] = "DEV"
                });
        });

        builder.ConfigureTestServices(services =>
        {
            services.Replace(ServiceDescriptor.Singleton(DateTimeProvider.Object));
            services.Replace(ServiceDescriptor.Singleton(AuditRepository.Object));
            services.Replace(ServiceDescriptor.Singleton(MonitorService.Object));
            services.Replace(ServiceDescriptor.Singleton(EhcoRflClient.Object));
        });
    }
}
