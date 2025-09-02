using Lyzer.Common.DTO;
using Lyzer.Services;

using Microsoft.AspNetCore.Mvc;

namespace Lyzer.Controllers
{
    [ApiController]
    [Route("api/v1/lyzer/overview")]
    public class OverviewController(OverviewService overviewService) : ControllerBase
    {
        private readonly OverviewService _overviewService = overviewService;

        [HttpGet("", Name = "GetOverviewData")]
        public async Task<OverviewDataDTO> GetOverviewData()
        {
            return await _overviewService.GetOverviewData();
        }
    }
}