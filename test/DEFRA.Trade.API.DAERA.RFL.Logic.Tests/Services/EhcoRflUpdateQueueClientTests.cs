// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using AutoFixture;
using Azure.Messaging.ServiceBus;
using DEFRA.Trade.API.DAERA.RFL.Logic.Models;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace DEFRA.Trade.API.DAERA.RFL.Services.Tests.Services;

public class EhcoRflUpdateQueueClientTests
{
    private readonly Mock<IServiceBusClientFactory> _factory = new(MockBehavior.Strict);
    private readonly Fixture _fixture = new();
    private readonly QueueOptions _options = new();
    private EhcoRflUpdateQueueClient _sut;

    private EhcoRflUpdateQueueClient Sut => _sut ??= new(new DateTimeParser(), _factory.Object, FakeOptionsMonitor(EhcoRflUpdateQueueClient.OptionsName, _options));

    [Fact]
    public async Task SendAsync_SendsTheCorrectMessageToTheServiceBusQueue_UsingAConnectionString()
    {
        // arrange
        _options.ConnectionString = "Endpoint=sb://not-a-real.servicebus.windows.net/;SharedAccessKeyName=MyKey;SharedAccessKey=abc=";
        _options.QueueName = "abc";

        var client = new Mock<ServiceBusClient>(MockBehavior.Strict, _options.ConnectionString);
        var queue = new Mock<ServiceBusSender>(MockBehavior.Strict);
        using var cts = new CancellationTokenSource();
        var message = _fixture.Create<RflUpdateRequest>();
        message.ExchangedDocument.Id.Content = "MyMessageId";
        message.ExchangedDocument.IssueDateTime = new()
        {
            Content = "200902132331+00:00",
            Format = "205"
        };
#pragma warning disable CA1869 // Cache and reuse 'JsonSerializerOptions' instances
        string bodyJson = JsonSerializer.Serialize(message, new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
#pragma warning restore CA1869 // Cache and reuse 'JsonSerializerOptions' instances
        Expression<Func<ServiceBusMessage, bool>> validateServiceBusMessage = x =>
            x.Body.ToString() == bodyJson
            && x.ContentType == "application/json"
            && x.Subject == "trade.remos.rfl.notification"
            && x.SessionId == "MyMessageId"
            && x.ApplicationProperties.Contains(new("EntityKey", "MyMessageId"))
            && x.ApplicationProperties.Contains(new("PublisherId", "TradeAPI"))
            && x.ApplicationProperties.Contains(new("SchemaVersion", "1"))
            && x.ApplicationProperties.Contains(new("TimestampUtc", 1234567860L));

        client.Setup(m => m.FullyQualifiedNamespace).Returns("not-a-real.servicebus.windows.net").Verifiable();
        client.Setup(m => m.Identifier).Returns("abcxyz").Verifiable();
        client.Setup(m => m.CreateSender(_options.QueueName)).Returns(queue.Object).Verifiable();
        _factory.Setup(m => m.Create(_options.ConnectionString, null)).Returns(client.Object).Verifiable();
        queue.Setup(m => m.SendMessageAsync(It.Is(validateServiceBusMessage), cts.Token)).Returns(Task.CompletedTask).Verifiable();

        // act
        await Sut.SendAsync(message, cts.Token);
    }

    private static IOptionsMonitor<T> FakeOptionsMonitor<T>(string optionsName, T options)
    {
        var result = new Mock<IOptionsMonitor<T>>(MockBehavior.Strict);
        result.Setup(m => m.Get(optionsName)).Returns(options);
        return result.Object;
    }
}
