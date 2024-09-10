// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using DEFRA.Trade.API.DAERA.RFL.Infrastructure;
using DEFRA.Trade.API.DAERA.RFL.Logic.Models;
using Microsoft.Extensions.Options;

namespace DEFRA.Trade.API.DAERA.RFL.V1.Controllers
{
    [ApiVersion("1")]
    [Route("/multiregioncheck")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MultiRegionCheckController(IOptions<ApimExternalApisSettings> apiSettings) : ControllerBase
    {
        private readonly IOptions<ApimExternalApisSettings> _apiSettings = apiSettings ?? throw new ArgumentNullException(nameof(apiSettings));

        [HttpGet()]
        [ProducesResponseType(typeof(HealthReportResponse), StatusCodes.Status200OK)]
        public string Index()
        {
            return _apiSettings.Value.TestApiUri;
        }
    }
}
