// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Models = DEFRA.Trade.API.DAERA.RFL.Logic.Models;

namespace DEFRA.Trade.API.DAERA.RFL.V1.Mapping;

public class RflUpdateRequestProfile : Profile
{
    public RflUpdateRequestProfile()
    {
        CreateMap<Dtos.FormattedDateTime, Models.FormattedDateTime>();
        CreateMap<Dtos.IdType, Models.IdType>();
        CreateMap<Dtos.Issuer, Models.Issuer>();
        CreateMap<Dtos.RflExchangeDocument, Models.RflExchangeDocument>();
        CreateMap<Dtos.RflOperatorAddressUpdate, Models.RflOperatorAddressUpdate>();
        CreateMap<Dtos.RflOperatorUpdate, Models.RflOperatorUpdate>();
        CreateMap<Dtos.RflUpdateRequest, Models.RflUpdateRequest>();
        CreateMap<Dtos.TextContainer, Models.TextContainer>();
        CreateMap<Dtos.TextType, Models.TextType>();
    }
}