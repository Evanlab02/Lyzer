using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using RestSharp;

namespace Lyzer_BE.API.Services.Concrete
{
    public class ScheduleService : IScheduleService
    {
        public async Task<List<EventDTO>>? GetFullSchedule()
        {
            var options = new RestClientOptions("http://ergast.com/api/f1/");
            var client = new RestClient(options);
            var request = new RestRequest($"current.json");
            var response = await client.GetAsync<ScheduleDTO>(request);
            return response.ScheduleData.ScheduleTable.Events;
        }

        public async Task<EventDTO>? GetNextOrCurrentEvent()
        {
            var options = new RestClientOptions("http://ergast.com/api/f1/");
            var client = new RestClient(options);
            var request = new RestRequest($"current.json");
            var response = await client.GetAsync<ScheduleDTO>(request);
            var events = response.ScheduleData.ScheduleTable.Events;

            EventDTO nextEvent = new EventDTO();
            var today = DateTime.Now;
            foreach (EventDTO eventDTO in events)
            {
                var endDateTime = DateTime.Parse(eventDTO.Date);
                if (endDateTime > today)
                {
                    nextEvent = eventDTO;
                    break;
                }
            }
            return nextEvent;
        }
    }
}
