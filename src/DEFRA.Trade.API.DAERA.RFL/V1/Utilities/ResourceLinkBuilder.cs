// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Common.Api.Dtos;
using DEFRA.Trade.API.DAERA.RFL.V1.Controllers;
using Microsoft.AspNetCore.Routing;

namespace DEFRA.Trade.API.DAERA.RFL.V1.Utilities;

public static class ResourceLinkExtensions
{
    public static ResourceBase AddLinkToUpdateRflInformation(this ResourceBase resource, LinkGenerator linkGenerator, string basePath)
    {
        string href = basePath + linkGenerator.GetPathByAction(
                          nameof(RflController.Update),
                          nameof(RflController).Replace("Controller", string.Empty).Replace("C925B478-311E-4596-B11C-D659E9C3B576", "{gcId}"));

        resource.Links =
        [
            new()
            {
                Href = href,
                Method = HttpMethods.Post,
                Rel = "rfl/update",
                Description = "Endpoint to update RFL information onto EHCO"
            }
        ];

        return resource;
    }

}

