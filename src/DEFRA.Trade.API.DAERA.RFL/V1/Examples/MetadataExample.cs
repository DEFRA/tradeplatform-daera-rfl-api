// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using DEFRA.Trade.API.DAERA.RFL.V1.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace DEFRA.Trade.API.DAERA.RFL.V1.Examples;

public class MetadataExample : IExamplesProvider<ServiceMetadata>
{
    public ServiceMetadata GetExamples()
    {
        return new ServiceMetadata
        {
            Links = []
        };
    }
}