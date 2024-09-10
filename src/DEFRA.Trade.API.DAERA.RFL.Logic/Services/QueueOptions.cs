// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Services;

public class QueueOptions : ServiceBusOptions
{
    public string QueueName { get; set; }
}