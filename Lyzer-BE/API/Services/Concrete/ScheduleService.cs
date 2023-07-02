using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Lyzer_BE.Database;
using MongoDB.Driver;

namespace Lyzer_BE.API.Services.Concrete
{
    public class ScheduleService : IScheduleService
    {
        private readonly MongoController<RaceWeekendDTO> _mongoController;

        public ScheduleService(bool testing = false)
        {
            if (!testing)
            {
                var today = DateTime.Now;
                var currentYear = today.Year;
                _mongoController = new MongoController<RaceWeekendDTO>("Schedules", currentYear.ToString());
            }
        }

        public async Task<List<RaceWeekendDTO>>? GetFullSchedule()
        {
            return await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);
        }

        public async Task<RaceWeekendDTO>? GetNextOrCurrentEvent()
        {
            var today = DateTime.Now;
            var twoYearsFromNow = today.AddYears(2);

            RaceWeekendDTO nextEvent = new()
            {
                Date = $"{twoYearsFromNow.Year}-01-01",
                Time = "00:00:00",
            };

            var events = await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);

            foreach (RaceWeekendDTO eventDTO in events)
            {
                var nextEventDatetime = DateTime.Parse($"{nextEvent.Date} {nextEvent.Time}").AddHours(2.5);
                var endDateTime = DateTime.Parse($"{eventDTO.Date} {eventDTO.Time}").AddHours(2.5);

                if (endDateTime > today && nextEventDatetime > endDateTime)
                {
                    nextEvent = eventDTO;
                }
            };

            if (nextEvent.Date == $"{twoYearsFromNow.Year}-01-01")
            {
                _mongoController.SetCollection((today.Year + 1).ToString());
                var nextYearEvents = await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);

                foreach (RaceWeekendDTO eventDTO in nextYearEvents)
                {
                    var nextEventDatetime = DateTime.Parse($"{nextEvent.Date} {nextEvent.Time}").AddHours(2.5);
                    var endDateTime = DateTime.Parse($"{eventDTO.Date} {eventDTO.Time}").AddHours(2.5);

                    if (endDateTime > today && nextEventDatetime > endDateTime)
                    {
                        nextEvent = eventDTO;
                    }
                };
            }

            return nextEvent;
        }
    }
}

