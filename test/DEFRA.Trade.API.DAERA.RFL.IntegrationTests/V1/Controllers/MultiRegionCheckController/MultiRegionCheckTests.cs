// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using DEFRA.Trade.API.DAERA.RFL.IntegrationTests.Infrastructure;

namespace DEFRA.Trade.API.DAERA.RFL.IntegrationTests.V1.Controllers.MultiRegionCheckController;

public class MultiRegionCheckTests(DaeraRflApplicationFactory<Startup> webApplicationFactory) : IClassFixture<DaeraRflApplicationFactory<Startup>>
{
    private const string DefaultClientIpAddress = "12.34.56.789";

    private readonly DaeraRflApplicationFactory<Startup> _webApplicationFactory = webApplicationFactory ?? throw new ArgumentNullException(nameof(webApplicationFactory));

    [Fact]
    public async Task MultiRegionCheck_WhenCalled_ShouldReturnTheRegion()
    {
        // Arrange
        var client = _webApplicationFactory.CreateClient();
        _webApplicationFactory.AddApimUserContextHeaders(client, Guid.Empty, DefaultClientIpAddress);
        string expectedResponse = "\"This is test string from North Europe\"";

        // Act
        var response = await client.GetAsync("multiregioncheck");

        // Assert
        string content = await response.Content.ReadAsStringAsync();
        Assert.Equal(expectedResponse, content);
    }

}
