// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Net.Http.Json;
using Defra.Trade.Common.ExternalApi.Auditing.Models.Enums;
using DEFRA.Trade.API.DAERA.RFL.IntegrationTests.Helpers;
using DEFRA.Trade.API.DAERA.RFL.IntegrationTests.Infrastructure;
using DEFRA.Trade.API.DAERA.RFL.V1.Dtos;
using Microsoft.AspNetCore.Http;

namespace DEFRA.Trade.API.DAERA.RFL.IntegrationTests.V1.Controllers.RflController;

public class UpdateTests(DaeraRflApplicationFactory<Startup> webApplicationFactory) : IClassFixture<DaeraRflApplicationFactory<Startup>>
{
    private const string DefaultClientIpAddress = "12.34.56.789";

    private readonly DaeraRflApplicationFactory<Startup> _webApplicationFactory = webApplicationFactory ?? throw new ArgumentNullException(nameof(webApplicationFactory));

    [Fact]
    public async Task Update_WhenCalled_ShouldReturnHealthy()
    {
        // Arrange
        var client = _webApplicationFactory.CreateClient();
        var clientId = Guid.NewGuid();
        var sentAt = DateTime.UtcNow;
        var request = GetValidRflUpdate();
        _webApplicationFactory.AddApimUserContextHeaders(client, clientId, DefaultClientIpAddress);

        // Act
        var response = await client.PostAsJsonAsync("rfl/update", request);

        // Assert
        string content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNull();
        _webApplicationFactory.AuditRepository.VerifyAuditLogged(AuditLogType.DaeraRflApiUpdate,
            clientId, null, HttpMethods.Post,
            "/rfl/update", null, StatusCodes.Status204NoContent, sentAt, true, false, DefaultClientIpAddress);
        _webApplicationFactory.EhcoRflClient.Verify(x => x.SendAsync(It.Is<Logic.Models.RflUpdateRequest>(x => IsEquivalentTo(request, x)), It.IsAny<CancellationToken>()));
    }

    private static RflUpdateRequest GetValidRflUpdate()
    {
        return new RflUpdateRequest
        {
            ExchangedDocument =
            new()
            {
                Id = new() { Content = "abc", SchemeId = "DAERA.CHIP.RFL.MessageId", },
                TypeCode = new() { Content = "1004", ListAgencyId = "6" },
                IssueDateTime = new() { Content = "abc", Format = "205" },
                Issuer =
                new()
                {
                    Id = new() { Content = "abc", SchemeId = "RMS" },
                    Name = new() { Content = "abc", LanguageId = "en" },
                    RoleCode = new() { Content = "GA", ListAgencyId = "6" }
                }
            },
            OperatorResponsibleForConsignment = new RflOperatorUpdate[]
            {
               new()
               {
                   Id = new() {Content = "abc"},
                   ClassificationCode = new() {Content = "C"},
                   Name = new() {Content = "abc", LanguageId = "en"},
                   PostalAddress = new()
                   {
                       CityName = new() {Content = "abc"},
                       CountryCode = new() {Content = "XI"},
                       CountryName =
                           new()
                           {
                               Content = "abc",
                               LanguageId = "en"
                           },
                       CountrySubDivisionCode =
                           new()
                           {
                               Content = "abc",
                               SchemeAgencyId = "5",
                           },
                       CountrySubDivisionName =
                           new()
                           {
                               Content = "abc",
                               LanguageId = "en"
                           },
                       LineOne = new() {Content = "abc"},
                       Postcode = new()
                       {
                           Content = "abc",
                           LanguageId = "abc"
                       },
                       TypeCode = new()
                       {
                           Content = "5",
                           ListAgencyId = "6"
                       }
                   }
               }
            }
        };
    }

    private static bool IsEquivalentTo<T1, T2>(T1 expected, T2 actual)
    {
        try
        {
            actual.Should().BeEquivalentTo(expected);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
