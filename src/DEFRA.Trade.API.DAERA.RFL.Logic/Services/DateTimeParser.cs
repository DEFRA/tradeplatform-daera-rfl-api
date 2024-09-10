// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Globalization;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Services;

public class DateTimeParser : IDateTimeParser
{
    public DateTimeOffset Parse(string value, string formatId)
    {
        return formatId switch
        {
            "205" => DateTimeOffset.ParseExact(value, "yyyyMMddHHmmzzz", CultureInfo.InvariantCulture),
            _ => throw new NotSupportedException("Unsupported datetime format " + formatId)
        };
    }
}