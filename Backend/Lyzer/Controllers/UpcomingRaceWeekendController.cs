using Lyzer.Common.DTO;
using Lyzer.Services;

using Microsoft.AspNetCore.Mvc;

namespace Lyzer.Controllers
{
    [ApiController]
    [Route("api/v1/lyzer/overview")]
    public class UpcomingRaceWeekendController : ControllerBase
    {
        private readonly UpcomingRaceWeekendService _upcomingRaceWeekendService;

        public UpcomingRaceWeekendController(UpcomingRaceWeekendService upcomingRaceWeekendService)
        {
            _upcomingRaceWeekendService = upcomingRaceWeekendService;
        }

        [HttpGet("", Name = "GetUpcomingRaceWeekend")]
        public async Task<UpcomingRaceWeekendDTO> GetUpcomingRaceWeekend()
        {
            return await _upcomingRaceWeekendService.GetUpcomingRaceWeekend("current");
        }
    }
}