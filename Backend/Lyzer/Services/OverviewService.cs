using Lyzer.Common.DTO;
using Lyzer.Common.Helpers;
using Lyzer.Errors;

namespace Lyzer.Services
{
    public class OverviewService(RacesService racesService, ResultsService resultsService)
    {
        private readonly RacesService _racesService = racesService;
        private readonly ResultsService _resultsService = resultsService;

        private static UpcomingRaceWeekendDTO GetUpcomingRaceWeekend(RaceDTO nextRace, RaceDTO previousRace)
        {
            DateTimeOffset firstSessionDateTime = nextRace.RaceStartDateTime.AddDays(-2);
            DateTimeOffset previousRaceDateTime = previousRace.RaceStartDateTime;
            DateTimeOffset nextRaceDateTime = nextRace.FirstPractice != null ? nextRace.FirstPractice.SessionDateTime : nextRace.RaceStartDateTime;
            SessionDTO? firstSession = nextRace.Sessions.FirstOrDefault();

            if (firstSession != null)
            {
                firstSessionDateTime = firstSession.SessionDateTime;
            }

            var isRaceWeekend = DateTimeHelper.IsOngoing(firstSessionDateTime.DateTime, nextRace.RaceStartDateTime.DateTime);
            var timeToRaceWeekend = DateTimeHelper.GetMinutesUntilDateTimeOffset(firstSessionDateTime);
            var timeToRaceWeekendProgress = DateTimeHelper.GetTimeProgressBetweenDateTimeOffsetsAsPercentage(previousRaceDateTime, nextRaceDateTime);

            var status = "No";

            switch (timeToRaceWeekendProgress)
            {
                case 100:
                    status = "It is race weekend!";
                    break;
                case int progress when progress >= 80:
                    status = "Almost";
                    break;
            }

            return new UpcomingRaceWeekendDTO()
            {
                IsRaceWeekend = isRaceWeekend,
                TimeToRaceWeekendProgress = timeToRaceWeekendProgress,
                Status = status,
                TimeToRaceWeekend = timeToRaceWeekend
            };
        }

        private static RaceWeekendProgressDTO GetRaceWeekendProgress(RaceDTO nextRace)
        {
            var nextSession = RacesHelper.GetNextRaceSession(nextRace);
            var weekendProgressPercentage = RacesHelper.GetWeekendProgressPercentage(nextRace);
            var isOngoing = DateTimeHelper.IsOngoing(nextSession.SessionDateTime, nextSession.SessionEndDateTime);

            return new RaceWeekendProgressDTO()
            {
                Name = nextSession.Name,
                Ongoing = isOngoing,
                WeekendProgress = weekendProgressPercentage,
                StartDateTime = nextSession?.SessionDateTime
            };
        }

        private async Task<SeasonProgressDTO> GetSeasonProgress(RacesDTO races, RaceDTO previousRace)
        {
            string season = previousRace.Season;
            string previousRound = previousRace.Round;

            ResultsDTO previousRaceResult = await _resultsService.GetCachedRaceResult(season, previousRound);
            DriverDTO lastRaceWinner = previousRaceResult.Results[0].Driver;

            return new SeasonProgressDTO
            {
                PreviousRaceWinner = $"{lastRaceWinner.GivenName} {lastRaceWinner.FamilyName}",
                PreviousGrandPrix = previousRace.RaceName,
                SeasonProgress = int.Parse(previousRace.Round),
                SeasonTotalRaces = races.Races.Count
            };
        }


        public async Task<OverviewDataDTO> GetOverviewData()
        {
            var races = await _racesService.GetCachedRaces("current");

            var previousRace = RacesHelper.GetPreviousRace(races);
            var nextRace = RacesHelper.GetNextOrCurrentRace(races);

            if (previousRace == null)
            {
                var now = DateTime.Now;
                var previousYear = now.AddYears(-1).Year.ToString();
                var previousYearRaces = await _racesService.GetCachedRaces(previousYear);
                previousRace = RacesHelper.GetPreviousRace(previousYearRaces);
            }

            if (previousRace == null)
            {
                throw new GeneralException("No previous race found.", StatusCodes.Status500InternalServerError);
            }

            if (nextRace == null)
            {
                throw new GeneralException("No upcoming race.", StatusCodes.Status404NotFound);
            }

            UpcomingRaceWeekendDTO upcomingRaceWeekend = GetUpcomingRaceWeekend(nextRace, previousRace);
            RaceWeekendProgressDTO raceWeekendProgress = GetRaceWeekendProgress(nextRace);
            SeasonProgressDTO seasonProgress = await GetSeasonProgress(races, previousRace);

            return new OverviewDataDTO
            {
                RaceWeekendProgress = raceWeekendProgress,
                UpcomingRaceWeekend = upcomingRaceWeekend,
                SeasonProgress = seasonProgress
            };
        }
    }
}