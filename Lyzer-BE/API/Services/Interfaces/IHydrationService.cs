using Lyzer_BE.API.DTOs;

namespace Lyzer_BE.API.Services.Interfaces
{
    public interface IHydrationService
    {
        public ScheduleDTO HydrateCurrentSchedule();
    }
}
