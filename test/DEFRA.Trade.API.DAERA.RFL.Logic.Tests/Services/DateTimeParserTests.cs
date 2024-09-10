// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using DEFRA.Trade.API.DAERA.RFL.Logic.Services;

namespace DEFRA.Trade.API.DAERA.RFL.Services.Tests.Services;

public class DateTimeParserTests
{
    private readonly DateTimeParser _sut = new();

    [Theory]
    [InlineData("202402271337+00:00", "205", 638446378200000000, 0)]
    [InlineData("202402271337+06:00", "205", 638446378200000000, 360)]
    public void Parse_CorrectlyParses(string value, string formatId, long expectedTicks, int expectedOffset)
    {
        // arrange
        var expected = new DateTimeOffset(expectedTicks, TimeSpan.FromMinutes(expectedOffset));

        // act
        var actual = _sut.Parse(value, formatId);

        // assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("202402271337+00:00", "204")]
    [InlineData("202402271337", "205")]
    public void Parse_FailsToParse(string value, string formatId)
    {
        // arrange
        var test = () => _sut.Parse(value, formatId);

        // act

        // assert
        test.Should().Throw<Exception>();
    }
}
