using Lyzer_BE.API.DTOs;

namespace Lyzer_BE.API.Services.Interfaces
{
    public interface IScheduleService
    {
        public Task<List<RaceWeekendDTO>>? GetFullSchedule(string year = "current");

        public Task<RaceWeekendDTO>? GetNextOrCurrentRaceWeekend();

        public Task<RaceWeekendDTO?> GetRaceWeekendByRound(string round, string year = "current");
    }
}
