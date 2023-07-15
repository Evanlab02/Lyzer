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
        private readonly IApiKeyService _apiKeyService;

        public HydrationController(IHydrationService hydrationService, IApiKeyService apiKeyService)
        {
            _hydrationService = hydrationService;
            _apiKeyService = apiKeyService;
        }

        // GET api/hydration/schedule
        [HttpPost("schedule/current")]
        public async Task<ScheduleDTO> HydrateCurrentSchedule(ApiKeyUserDTO apiToken)
        {
            var auth = await _apiKeyService.VerifyToken(apiToken);
            if (!auth.ValidToken)
            {
                return new ScheduleDTO();
            }

            var year = DateTime.Now.Year;
            return await _hydrationService.HydrateSchedule(year.ToString());
        }

        [HttpPost("schedule/next")]
        public async Task<ScheduleDTO> HydrateFollowingYearSchedule(ApiKeyUserDTO apiToken)
        {
            var auth = await _apiKeyService.VerifyToken(apiToken);
            if (!auth.ValidToken)
            {
                return new ScheduleDTO();
            }

            var year = DateTime.Now.Year + 1;
            return await _hydrationService.HydrateSchedule(year.ToString());
        }
    }
}
