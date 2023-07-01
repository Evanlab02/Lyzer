using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Lyzer_BE.Database;
using RestSharp;

namespace Lyzer_BE.API.Services.Concrete
{
    public class ScheduleService : IScheduleService
    {
        private readonly MongoController<RaceWeekendDTO> _mongoController;
        public ScheduleService()
        {
            _mongoController = new MongoController<RaceWeekendDTO>("Schedules");
        }
        public async Task<List<RaceWeekendDTO>>? GetFullSchedule()
        {
            var options = new RestClientOptions("http://ergast.com/api/f1/");
            var client = new RestClient(options);
            var request = new RestRequest($"current.json");
            var response = await client.GetAsync<ScheduleDTO>(request);
            return response.ScheduleData.ScheduleTable.RaceWeekends;
        }

        public async Task<RaceWeekendDTO>? GetNextOrCurrentEvent()
        {
            var options = new RestClientOptions("http://ergast.com/api/f1/");
            var client = new RestClient(options);
            var request = new RestRequest($"current.json");
            var response = await client.GetAsync<ScheduleDTO>(request);
            var events = response.ScheduleData.ScheduleTable.RaceWeekends;

            RaceWeekendDTO nextEvent = new RaceWeekendDTO();
            var today = DateTime.Now;
            foreach (RaceWeekendDTO eventDTO in events)
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
