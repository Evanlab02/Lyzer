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

        public OverviewController(RacesService raceService)
        {
            _raceService = raceService;
        }

        [HttpGet("", Name = "GetUpcomingRaceWeekend")]
        public async Task<UpcomingRaceWeekendDTO> GetUpcomingRaceWeekend()
        {
            return await _raceService.GetUpcomingRaceWeekend("current");
        }
    }
}