using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Lyzer_BE.Database;
using MongoDB.Driver;

namespace Lyzer_BE.API.Services.Concrete
{
    public class ScheduleService : IScheduleService
    {
        private readonly MongoController<RaceWeekendDTO> _mongoControllerCurrent;
        private readonly MongoController<RaceWeekendDTO> _mongoControllerAll;
        public ScheduleService(bool testing = false)
        {
            if (!testing)
            {
                _mongoControllerCurrent = new MongoController<RaceWeekendDTO>("Current Schedules");
                _mongoControllerAll = new MongoController<RaceWeekendDTO>("All Schedules");
            }
        }

        public async Task<List<RaceWeekendDTO>>? GetFullSchedule()
        {
            return await _mongoControllerCurrent.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);
        }

        public async Task<RaceWeekendDTO>? GetNextOrCurrentEvent()
        {
            var events = await _mongoControllerCurrent.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);

            RaceWeekendDTO nextEvent = new RaceWeekendDTO();
            var today = DateTime.Now;
            foreach (RaceWeekendDTO eventDTO in events)
            {
                var endDateTime = DateTime.Parse($"{eventDTO.Date} {eventDTO.Time}").AddHours(2.5);
                if (endDateTime > today)
                {
                    nextEvent = eventDTO;
                    break;
                }
            }
            //Edge case: someway to account for the last race, what do we return then?

            return nextEvent;
        }
    }
}
