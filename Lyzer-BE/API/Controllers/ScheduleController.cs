using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lyzer_BE.API.Controllers
{
    [Route("api/schedule")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {

        private readonly IScheduleService _scheduleService;
        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        // GET api/schedule
        [HttpGet("current")]
        public Task<List<EventDTO>>? GetSchedule()
        {
            return _scheduleService.GetFullSchedule();
        }

        [HttpGet("next")]
        public Task<EventDTO>? GetNextEvent()
        {
            return _scheduleService.GetNextOrCurrentEvent();
        }

    }
}
