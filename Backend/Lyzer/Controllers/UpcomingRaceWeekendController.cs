using Lyzer.Common.DTO;
using Lyzer.Services;

using Microsoft.AspNetCore.Mvc;

namespace Lyzer.Controllers
{
    [ApiController]
    [Route("api/v1/lyzer/overview")]
    public class UpcomingRaceWeekendController : ControllerBase
    {
        private readonly ILogger<UpcomingRaceWeekendController> _logger;
        private readonly UpcomingRaceWeekendService _upcomingRaceWeekendService;

        public UpcomingRaceWeekendController(ILogger<UpcomingRaceWeekendController> logger, UpcomingRaceWeekendService upcomingRaceWeekendService)
        {
            _logger = logger;
            _upcomingRaceWeekendService = upcomingRaceWeekendService;
        }

        [HttpGet("current", Name = "GetUpcomingRaceWeekend")]
        public async Task<UpcomingRaceWeekendDTO> GetUpcomingRaceWeekend()
        {
            return await _upcomingRaceWeekendService.GetUpcomingRaceWeekend("current");
        }
    }
}