using Lyzer_BE.API.DTOs;

namespace Lyzer_BE.API.Services.Interfaces
{
    public interface IHydrationService
    {
        public Task<ScheduleDTO> HydrateCurrentSchedule();
        public Task<ScheduleDTO> HydrateFollowingYearSchedule();
        public Task<ScheduleDTO> HydrateSchedule(string year);
    }
}
