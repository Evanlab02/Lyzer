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
            if (
                !year.Equals("current") &&
                !String.IsNullOrEmpty(year) &&
                Int32.Parse(year) >= 1950 &&
                Int32.Parse(year) <= DateTime.Now.AddYears(1).Year
            )
            {
                var collectionExists = await _mongoController.SetCollection(year);

                var hydratedSchedule = new ScheduleDTO();

                if (!collectionExists)
                {
                    hydratedSchedule = await _hydrationService.HydrateSchedule(year, _mongoController);
                }

                List<RaceWeekendDTO> schedule = new();

                if (!collectionExists)
                {
                    schedule = hydratedSchedule.ScheduleData.ScheduleTable.RaceWeekends;
                }
                else
                {
                    schedule = await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);
                }


                if (schedule.Count == 0)
                {
                    var result = await _hydrationService.HydrateSchedule(year);
                    schedule = result.ScheduleData.ScheduleTable.RaceWeekends;
                }

                await _mongoController.SetCollection(DateTime.Now.Year.ToString());

                return schedule;
            }
            else if (year.Equals("current"))
            {
                return await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);
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

            var raceWeekends = await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);

            foreach (RaceWeekendDTO raceWeekendDTO in raceWeekends)
            {
                var nextRWDatetime = DateTime.Parse($"{nextRaceWeekend.Date} {nextRaceWeekend.Time}").AddHours(2.5);
                var endDateTime = DateTime.Parse($"{raceWeekendDTO.Date} {raceWeekendDTO.Time}").AddHours(2.5);

                if (endDateTime > today && nextRWDatetime > endDateTime)
                {
                    nextRaceWeekend = raceWeekendDTO;
                }
            };

            if (nextRaceWeekend.Date == $"{twoYearsFromNow.Year}-01-01")
            {
                await _mongoController.SetCollection((today.Year + 1).ToString());
                var nextYearRaceWeekends = await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);

                foreach (RaceWeekendDTO raceWeekendDTO in nextYearRaceWeekends)
                {
                    var nextRWDatetime = DateTime.Parse($"{nextRaceWeekend.Date} {nextRaceWeekend.Time}").AddHours(2.5);
                    var endDateTime = DateTime.Parse($"{raceWeekendDTO.Date} {raceWeekendDTO.Time}").AddHours(2.5);

                    if (endDateTime > today && nextRWDatetime > endDateTime)
                    {
                        nextRaceWeekend = raceWeekendDTO;
                    }
                };
            }

            return nextRaceWeekend;
        }
    }
}

