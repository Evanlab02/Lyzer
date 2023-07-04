using Lyzer_BE.API.DTOs;
using RestSharp;

namespace Lyzer_BE.API.Services.Interfaces
{
    public interface IHydrationService
    {
        public Task<ScheduleDTO> HydrateSchedule(string year);
    }
}
