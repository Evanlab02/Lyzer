using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lyzer_BE.API.Controllers
{
    [Route("api/hydrate")]
    [ApiController]
    public class HydrationController : ControllerBase
    {
        private readonly IHydrationService _hydrationService;

        public HydrationController(IHydrationService hydrationService)
        {
            _hydrationService = hydrationService;
        }

        // GET api/hydration/schedule
        [HttpGet("schedule/current")]
        public Task<ScheduleDTO> HydrateCurrentSchedule()
        {
            var year = DateTime.Now.Year;
            return _hydrationService.HydrateSchedule(year.ToString());
        }

        [HttpGet("schedule/next")]
        public Task<ScheduleDTO> HydrateFollowingYearSchedule()
        {
            var year = DateTime.Now.Year + 1;
            return _hydrationService.HydrateSchedule(year.ToString());
        }
    }
}
