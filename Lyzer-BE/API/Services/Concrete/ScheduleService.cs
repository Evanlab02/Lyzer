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

        public async Task<List<RaceWeekendDTO>>? GetFullSchedule(string year)
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
                List<RaceWeekendDTO> schedule = await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);

                if (schedule.Count == 0)
                {
                    var result = await _hydrationService.HydrateSchedule(year);
                    schedule = result.ScheduleData.ScheduleTable.RaceWeekends;
                }

                return schedule;
            }
            //Throw some exception once exception handler is created.
            return new List<RaceWeekendDTO>();
        }

        public async Task<RaceWeekendDTO>? GetNextOrCurrentRaceWeekend()
        {
            var today = DateTime.Now;
            var twoYearsFromNow = today.AddYears(2);

            RaceWeekendDTO nextRaceWeekend = new()
            {
                Date = $"{twoYearsFromNow.Year}-01-01",
                Time = "00:00:00",
            };

            var currentYearRaceWeekends = await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);
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

