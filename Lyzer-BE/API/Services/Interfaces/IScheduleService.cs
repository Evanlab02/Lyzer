using Lyzer_BE.API.DTOs;

namespace Lyzer_BE.API.Services.Interfaces
{
    public interface IScheduleService
    {
        public Task<List<RaceWeekendDTO>>? GetFullSchedule();

        public Task<RaceWeekendDTO>? GetNextOrCurrentEvent();
    }
}
