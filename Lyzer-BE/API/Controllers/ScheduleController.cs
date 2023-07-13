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

        [HttpGet("current")]
        public Task<List<RaceWeekendDTO>>? GetCurrent()
        {
            return _scheduleService.GetFullSchedule();
        }

        [HttpGet("{year}")]
        public Task<List<RaceWeekendDTO>>? GetScheduleForYear(string year)
        {
            return _scheduleService.GetFullSchedule(year);
        }

        [HttpGet("current/raceweekend/next")]
        public Task<RaceWeekendDTO>? GetNextRaceWeekend()
        {
            return _scheduleService.GetNextOrCurrentRaceWeekend();
        }

        [HttpGet("current/raceweekend/{round}")]
        public Task<RaceWeekendDTO?> GetRaceWeekendByRound(string round)
        {
            return _scheduleService.GetRaceWeekendByRound(round);
        }

        [HttpGet("{year}/raceweekend/{round}")]
        public Task<RaceWeekendDTO?> GetRaceWeekendByRoundAndYear(string round, string year)
        {
            return _scheduleService.GetRaceWeekendByRound(round);
        }
    }
}
