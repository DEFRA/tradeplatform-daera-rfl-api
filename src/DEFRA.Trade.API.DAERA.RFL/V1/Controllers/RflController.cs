// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Common.Api.Dtos;
using Defra.Trade.Common.ExternalApi.Auditing;
using Defra.Trade.Common.ExternalApi.Auditing.Models.Enums;
using DEFRA.Trade.API.DAERA.RFL.Logic.Extensions;
using DEFRA.Trade.API.DAERA.RFL.Logic.Services.Interfaces;
using DEFRA.Trade.API.DAERA.RFL.V1.Dtos;
using Models = DEFRA.Trade.API.DAERA.RFL.Logic.Models;

namespace DEFRA.Trade.API.DAERA.RFL.V1.Controllers;

/// <summary>
/// Responsible For Load controller.
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("rfl")]
public class RflController(
    IRflService rfl,
    IMapper mapper,
    ILogger<RflController> logger) : ControllerBase
{
    private readonly ILogger<RflController> _logger = logger;

    /// <summary>
    /// Updates the list of responsible for load operators
    /// </summary>
    /// <response code="200">Successfully queued the update request.</response>
    [HttpPost("update", Name = "SetRflEntities")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CommonProblemDetails), StatusCodes.Status400BadRequest)]
    [Audit(LogType = AuditLogType.DaeraRflApiUpdate, WithRequestBody = true, WithResponseBody = true)]
    public async Task<IActionResult> Update([FromBody] RflUpdateRequest request, CancellationToken cancellationToken)
    {
        string requestDocId = request.ExchangedDocument.Id.Content;
        _logger.RflUpdateRequestStart(requestDocId);

        var entities = mapper.Map<Models.RflUpdateRequest>(request);
        await rfl.SetRflEntities(entities, cancellationToken);

        _logger.RflUpdateRequestSuccess(requestDocId);

        return NoContent();
    }
}
