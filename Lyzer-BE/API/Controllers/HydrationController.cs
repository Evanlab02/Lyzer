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
        [HttpGet("schedule")]
        public Task<ScheduleDTO> HydrateCurrentSchedule()
        {
            return _hydrationService.HydrateCurrentSchedule();
        }
    }
}
