// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Azure.Identity;
using Azure.Messaging.ServiceBus;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services;

namespace DEFRA.Trade.API.DAERA.RFL.Services.Tests.Services;

public class ServiceBusClientFactoryTests
{
    private readonly ServiceBusClientFactory _sut = new();

    [Fact]
    public void Create_FromConnectionString_IsSuccessful()
    {
        // arrange
        string connectionString = "Endpoint=sb://not-a-real.servicebus.windows.net/;SharedAccessKeyName=MyKey;SharedAccessKey=abc=";
        var options = new ServiceBusClientOptions
        {
            Identifier = "123",
            TransportType = ServiceBusTransportType.AmqpTcp
        };

        // act
        var result = _sut.Create(connectionString: connectionString, options: options);

        // assert
        result.FullyQualifiedNamespace.Should().Be("not-a-real.servicebus.windows.net");
        result.Identifier.Should().Be(options.Identifier);
        result.TransportType.Should().Be(options.TransportType);
    }

    [Fact]
    public void Create_FromFQNamespaceAndTokenCredential_IsSuccessful()
    {
        // arrange
        string fullyQualifiedNamespace = "not-a-real.servicebus.windows.net";
        var credential = new DefaultAzureCredential();
        var options = new ServiceBusClientOptions
        {
            Identifier = "123",
            TransportType = ServiceBusTransportType.AmqpTcp
        };

        // act
        var result = _sut.Create(fullyQualifiedNamespace: fullyQualifiedNamespace, credential: credential, options: options);

        // assert
        result.FullyQualifiedNamespace.Should().Be("not-a-real.servicebus.windows.net");
        result.Identifier.Should().Be(options.Identifier);
        result.TransportType.Should().Be(options.TransportType);
    }
}
