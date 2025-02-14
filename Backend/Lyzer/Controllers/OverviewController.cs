using Lyzer.Common.DTO;
using Lyzer.Services;

using Microsoft.AspNetCore.Mvc;

namespace Lyzer.Controllers
{
    [ApiController]
    [Route("api/v1/lyzer/overview")]
    public class OverviewController : ControllerBase
    {
        private readonly RacesService _raceService;
        private readonly OverviewService _overviewService;

        public OverviewController(RacesService raceService, OverviewService overviewService)
        {
            _raceService = raceService;
            _overviewService = overviewService;
        }

        [HttpGet("raceweekend/upcoming", Name = "GetUpcomingRaceWeekend")]
        public async Task<UpcomingRaceWeekendDTO> GetUpcomingRaceWeekend()
        {
            return await _raceService.GetUpcomingRaceWeekend("current");
        }

        [HttpGet("", Name = "GetOverviewData")]
        public async Task<OverviewDataDTO> GetOverviewData()
        {
            return await _overviewService.GetOverviewData();
        }
    }
}