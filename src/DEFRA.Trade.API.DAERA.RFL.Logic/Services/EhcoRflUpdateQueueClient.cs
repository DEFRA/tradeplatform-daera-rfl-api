// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Text.Json.Serialization;
using Azure.Messaging.ServiceBus;
using DEFRA.Trade.API.DAERA.RFL.Logic.Models;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Services;

public class EhcoRflUpdateQueueClient : ServiceBusQueueClient<RflUpdateRequest>
{
    public const string OptionsName = "ehco-rfl-queue";
    private readonly IDateTimeParser _dateTimeParser;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public EhcoRflUpdateQueueClient(IDateTimeParser dateTimeParser, IServiceBusClientFactory factory, IOptionsMonitor<QueueOptions> options)
        : base(factory, options.Get(OptionsName))
    {
        ArgumentNullException.ThrowIfNull(dateTimeParser);
        _dateTimeParser = dateTimeParser;
    }

    protected override ServiceBusMessage CreateMessage(RflUpdateRequest message)
    {
        var issueDateTime = message.ExchangedDocument.IssueDateTime;
        var timestamp = _dateTimeParser.Parse(issueDateTime.Content, issueDateTime.Format);

        return new(JsonSerializer.Serialize(message, _jsonOptions))
        {
            ContentType = "application/json",
            Subject = "trade.remos.rfl.notification",
            SessionId = message.ExchangedDocument.Id.Content,
            ApplicationProperties =
            {
                ["EntityKey"] = message.ExchangedDocument.Id.Content,
                ["PublisherId"] = "TradeAPI",
                ["SchemaVersion"] = "1",
                ["TimestampUtc"] = timestamp.ToUnixTimeSeconds()
            }
        };
    }
}
