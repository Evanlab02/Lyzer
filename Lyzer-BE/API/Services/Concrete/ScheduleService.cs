using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Lyzer_BE.Database;
using MongoDB.Driver;

namespace Lyzer_BE.API.Services.Concrete
{
    public class ScheduleService : IScheduleService
    {
        private readonly MongoController<RaceWeekendDTO> _mongoController;
        private readonly IHydrationService _hydrationService;

        public ScheduleService(IHydrationService hydrationService)
        {
            _mongoController = new MongoController<RaceWeekendDTO>("Schedules", DateTime.Now.Year.ToString());
            _hydrationService = hydrationService;
        }

        public async Task<List<RaceWeekendDTO>>? GetFullSchedule(string year = "current")
        {
            if (year.Equals("current"))
            {
                year = DateTime.Now.Year.ToString();
            }

            if (
                !string.IsNullOrEmpty(year) &&
                int.Parse(year) >= 1950 &&
                int.Parse(year) <= DateTime.Now.AddYears(1).Year
            )
            {
                _mongoController.SetCollection(year);
                var CollectionExists = _mongoController.CollectionExists();

                if (!CollectionExists)
                {
                    var result = await _hydrationService.HydrateSchedule(year);
                    return result.ScheduleData.ScheduleTable.RaceWeekends;
                }
                else
                {
                    return await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);
                }
            }
            //Throw some exception once exception handler is created.
            return new List<RaceWeekendDTO>();
        }

        public async Task<RaceWeekendDTO>? GetNextOrCurrentRaceWeekend()
        {
            var today = DateTime.Now;
            var yearFromNow = today.AddYears(1).Year.ToString();
            var twoYearsFromNow = today.AddYears(2);

            RaceWeekendDTO nextRaceWeekend = new()
            {
                Date = $"{twoYearsFromNow.Year}-01-01",
                Time = "00:00:00",
            };

            if (!_mongoController.CollectionExists())
            {
                await _hydrationService.HydrateSchedule(today.Year.ToString());
            };

            var currentYearRaceWeekends = await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);
            _mongoController.SetCollection(yearFromNow);

            if (!_mongoController.CollectionExists())
            {
                await _hydrationService.HydrateSchedule(yearFromNow);
            }

            var nextYearRaceWeekends = await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);
            var allRaceWeekends = currentYearRaceWeekends.Concat(nextYearRaceWeekends).ToList();

            foreach (RaceWeekendDTO raceWeekendDTO in allRaceWeekends)
            {
                var nextRWDatetime = DateTime.Parse($"{nextRaceWeekend.Date} {nextRaceWeekend.Time}").AddHours(2.5);
                var endDateTime = DateTime.Parse($"{raceWeekendDTO.Date} {raceWeekendDTO.Time}").AddHours(2.5);

                if (endDateTime > today && nextRWDatetime > endDateTime)
                {
                    nextRaceWeekend = raceWeekendDTO;
                }
            };

            return nextRaceWeekend;
        }
    }
}

