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
            year = GetCurrentYearIfCurrent(year);

            if (IsYearValid(year))
            {
                _mongoController.SetCollection(year);

                if (!_mongoController.CollectionExists())
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

            List<RaceWeekendDTO> currentYearRaceWeekends;
            List<RaceWeekendDTO> nextYearRaceWeekends;

            if (!_mongoController.CollectionExists(today.Year.ToString()) && !_mongoController.CollectionExists(yearFromNow))
            {
                var currentYearResponse = await _hydrationService.HydrateSchedule(today.Year.ToString());
                var followingYearResponse = await _hydrationService.HydrateSchedule(yearFromNow);
                currentYearRaceWeekends = currentYearResponse.ScheduleData.ScheduleTable.RaceWeekends;
                nextYearRaceWeekends = followingYearResponse.ScheduleData.ScheduleTable.RaceWeekends;
            }
            else
            {
                _mongoController.SetCollection(today.Year.ToString());
                currentYearRaceWeekends = await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);
                _mongoController.SetCollection(yearFromNow);
                nextYearRaceWeekends = await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);
            }

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

        public async Task<RaceWeekendDTO?> GetRaceWeekendByRound(string round, string year = "current")
        {
            year = GetCurrentYearIfCurrent(year);

            if (IsYearValid(year))
            {
                _mongoController.SetCollection(year);

                if (!_mongoController.CollectionExists())
                {
                    var result = await _hydrationService.HydrateSchedule(year);
                    return result
                        .ScheduleData
                        .ScheduleTable
                        .RaceWeekends
                        .Where(x => x.Round.Equals(round))
                        .FirstOrDefault();
                }
                else
                {
                    return await _mongoController
                        .FindOneFromCollection(Builders<RaceWeekendDTO>.Filter.Where(x => x.Round.Equals(round)));
                }
            }
            //Throw some exception once exception handler is created.
            return new RaceWeekendDTO();
        }

        /// <summary>
        /// <param name="year">String to check if current</param>
        /// <returns>Current Date Year if the string is "current" or returns the same value</returns>
        /// </summary>
        private string GetCurrentYearIfCurrent(string year)
        {
            return year == "current" ? DateTime.Now.Year.ToString() : year;
        }

        /// <summary>
        /// <param name="year">Year being validated</param>
        /// <returns>Bool depending of if the year matches the valid criteria.</returns>
        /// </summary>
        private bool IsYearValid(string year)
        {
            return !String.IsNullOrEmpty(year) &&
                    int.Parse(year) >= 1950 &&
                    int.Parse(year) <= DateTime.Now.AddYears(1).Year;
        }
    }
}

