// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using DEFRA.Trade.API.DAERA.RFL.Logic.Models;

namespace DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;

public interface IRflService
{
    Task SetRflEntities(RflUpdateRequest request, CancellationToken cancellationToken);
}