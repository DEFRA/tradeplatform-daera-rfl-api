// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Common.Api.Dtos;
using Defra.Trade.Common.ExternalApi.Auditing.Models.Enums;
using DEFRA.Trade.API.DAERA.RFL.IntegrationTests.Helpers;
using DEFRA.Trade.API.DAERA.RFL.IntegrationTests.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace DEFRA.Trade.API.DAERA.RFL.IntegrationTests.V1.Controllers.MetadataController;

public class GetTests(DaeraRflApplicationFactory<Startup> webApplicationFactory) : IClassFixture<DaeraRflApplicationFactory<Startup>>
{
    private const string DefaultClientIpAddress = "12.34.56.789";

    [Fact]
    public async Task Get_Default_OK()
    {
        // Arrange
        var client = webApplicationFactory.CreateClient();
        var clientId = Guid.NewGuid();

        webApplicationFactory.AddApimUserContextHeaders(client, clientId, DefaultClientIpAddress);

        var sentAt = DateTime.UtcNow;

        // Act
        var response = await client.GetAsync("metadata");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsAsync<DEFRA.Trade.API.DAERA.RFL.V1.Dtos.ServiceMetadata>();

        content.Links.Should().HaveCount(1, "because 1 link is required");


        webApplicationFactory.AuditRepository.VerifyAuditLogged(AuditLogType.DaeraRflApiMetaData,
            clientId, null, HttpMethods.Get,
            "/metadata", null, StatusCodes.Status200OK, sentAt, false, false, DefaultClientIpAddress);
    }

    [Fact]
    public async Task Get_MissingClientId_Forbidden()
    {
        // Arrange
        var client = webApplicationFactory.CreateClient();

        webApplicationFactory.AddApimUserContextHeaders(client, Guid.Empty, DefaultClientIpAddress);

        // Act
        var response = await client.GetAsync("metadata");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

        var content = await response.Content.ReadAsAsync<CommonProblemDetails>();

        content.VerifyForbidden();
    }
}
