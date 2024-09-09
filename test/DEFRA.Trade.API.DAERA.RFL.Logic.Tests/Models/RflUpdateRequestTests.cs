// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Text.Json;
using System.Text.Json.Serialization;
using DEFRA.Trade.API.DAERA.RFL.Logic.Models;
using Newtonsoft.Json.Linq;

namespace DEFRA.Trade.API.DAERA.RFL.Services.Tests.Models;

public static class RflUpdateRequestTests
{
    private static readonly JsonSerializerOptions _options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    [Theory]
    [MemberData(nameof(SerializeAsJson_MatchesSchema_TheoryData))]
    public static void SerializeAsJson_MatchesSchema(string json)
    {
        // arrange
        var expected = JToken.Parse(json);

        // act
        var model = JsonSerializer.Deserialize<RflUpdateRequest>(json, _options);
        string serialized = JsonSerializer.Serialize(model, _options);
        var actual = JToken.Parse(serialized);

        // assert
        actual.Should().BeEquivalentTo(expected);
    }

    public static TheoryData<string> SerializeAsJson_MatchesSchema_TheoryData()
    {
        return new TheoryData<string>()
        {
            """
            {
              "ExchangedDocument": {
                "ID": {
                  "content": "12345678",
                  "schemeID": "DAERA.CHIP.RFL.MessageId"
                },
                "TypeCode": {
                  "content": "1004",
                  "listAgencyID": "6"
                },
                "IssueDateTime": {
                  "content": "202306190000+0200",
                  "format": "205"
                },
                "Issuer": {
                  "Name": {
                      "content": "DAERA"
                    }
                }
              },
              "OperatorResponsibleForConsignment": [
                {
                  "ID":
                    {
                      "content": "DAERA1234",
                      "schemeID": "Operator_activity_id",
                      "schemeName": "Operator activity ID"
                    },
                  "Name":
                    {
                      "content": "Some Company Ltd"
                    },
                  "ClassificationCode":
                    {
                    "content": "C"
                    },
                  "PostalAddress": {
                    "Postcode": {
                      "content": "AB1 2CD"
                    },
                    "LineOne": {
                      "content": "123 Some Road"
                    },
                    "CityName": {
                      "content": "Some City"
                    },
                    "CountryCode": {
                      "content": "XI"
                    }
                  }
                }
              ]
            }
            """
        };
    }
}
