using Lyzer_BE.API.DTOs;
using Lyzer_BE.Database;

namespace Lyzer_BE.API.Services.Interfaces
{
    public interface IHydrationService
    {
        public Task<ScheduleDTO> HydrateCurrentSchedule();
        public Task<ScheduleDTO> HydrateFollowingYearSchedule();
        public Task<ScheduleDTO> HydrateSchedule(string year);
        public Task<ScheduleDTO> HydrateSchedule(string year, MongoController<RaceWeekendDTO>? mongoController);
    }
}
