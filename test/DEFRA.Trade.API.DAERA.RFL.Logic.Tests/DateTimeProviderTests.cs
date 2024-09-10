// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using DEFRA.Trade.API.DAERA.RFL.Logic.Services;

namespace DEFRA.Trade.API.DAERA.RFL.Services.Tests;

public class DateTimeProviderTests
{
    [Fact]
    public void Now_WhenCalled_ReturnsUtcNow()
    {
        // Arrange
        var sut = new DateTimeProvider();

        // Act
        var utcNow = DateTimeOffset.UtcNow;
        var result = sut.Now;

        // Assert
        result.Should().BeBefore(utcNow.AddSeconds(2));
        result.Should().BeAfter(utcNow.AddSeconds(-2));
    }
}