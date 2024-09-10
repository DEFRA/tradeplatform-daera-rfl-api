// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;

public interface IDateTimeParser
{
    DateTimeOffset Parse(string value, string formatId);
}