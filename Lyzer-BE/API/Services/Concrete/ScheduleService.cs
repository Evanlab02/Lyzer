using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Lyzer_BE.Database;
using MongoDB.Driver;

namespace Lyzer_BE.API.Services.Concrete
{
    public class ScheduleService : IScheduleService
    {
        private readonly MongoController<RaceWeekendDTO> _mongoController;
        private readonly INotificationService notificationService;

        public ScheduleService(INotificationService notificationService)
        {
            _mongoController = new MongoController<RaceWeekendDTO>("Schedules", DateTime.Now.Year.ToString());
            this.notificationService = notificationService;
        }

        public async Task<List<RaceWeekendDTO>>? GetFullSchedule(string year = "current")
        {
            year = GetCurrentYearIfCurrent(year);

            if (IsYearValid(year))
            {
                CheckIfScheduleExists(year);

                return await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);
            }

            throw new Exception($"Invalid year received for GetFullSchedule: {year}");
        }

        public async Task<RaceWeekendDTO>? GetNextOrCurrentRaceWeekend()
        {
            notificationService.SendAlert(new AlertDto()
            {
                Message = "SEBASTIAN VETTEL! DU BIST WELTMEISTER!!",
                Exception = "Testy Exception we are working good good :) or maybe not, not sure, we will see"
            }, Enums.AlertLevel.NonCritical);

            var today = DateTime.Now;
            var yearFromNow = today.AddYears(1).Year.ToString();
            var twoYearsFromNow = today.AddYears(2);

            RaceWeekendDTO nextRaceWeekend = new()
            {
                Date = $"{twoYearsFromNow.Year}-01-01",
                Time = "00:00:00",
            };

            CheckIfScheduleExists(today.Year.ToString(), $"Current year's schedule is unavailable. ({today.Year.ToString()})");
            List<RaceWeekendDTO> currentYearRaceWeekends = await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);

            CheckIfScheduleExists(yearFromNow, $"The following year's schedule is not yet available. ({yearFromNow})");
            List<RaceWeekendDTO> nextYearRaceWeekends = await _mongoController.FindManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);

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

            if (IsYearValid(year) && int.TryParse(round, out _))
            {
                CheckIfScheduleExists(year);

                var result = await _mongoController
                    .FindOneFromCollection(Builders<RaceWeekendDTO>.Filter.Where(x => x.Round.Equals(round)));

                return result != null ? result : new RaceWeekendDTO();
            }

            throw new Exception($"Invalid year or round received for GetRaceWeekendByRound: {year}/{round}");
        }

        private string GetCurrentYearIfCurrent(string year)
        {
            return year == "current" ? DateTime.Now.Year.ToString() : year;
        }

        private bool IsYearValid(string year)
        {
            return !String.IsNullOrEmpty(year) &&
                    int.TryParse(year, out _) &&
                    int.Parse(year) >= 1950 &&
                    int.Parse(year) <= DateTime.Now.AddYears(1).Year;
        }

        private void CheckIfScheduleExists(string year, string customMessage = "")
        {
            string errorMsg = String.IsNullOrEmpty(customMessage)
                                ? customMessage : $"Race schedule does not exist on Mongo for year: {year}";

            if (!_mongoController.CollectionExists(year))
                throw new Exception(errorMsg);

            _mongoController.SetCollection(year);
        }
    }
}

