using Lyzer.Common.Constants;
using Lyzer.Common.Constants.Colors;
using Lyzer.Common.DTO;
using Lyzer.Common.Helpers;
using Lyzer.Errors;

using Newtonsoft.Json;

namespace Lyzer.Services
{
    /// <summary>
    /// Service responsible for aggregating and providing overview data for the F1 dashboard.
    /// </summary>
    public class OverviewService(CacheService cacheService, RacesService racesService, ResultsService resultsService, DriverService driverService)
    {
        private readonly CacheService _cacheService = cacheService;
        private readonly RacesService _racesService = racesService;
        private readonly ResultsService _resultsService = resultsService;
        private readonly DriverService _driverService = driverService;

        /// <summary>
        /// Calculates information about the upcoming race weekend.
        /// </summary>
        /// <param name="nextRace">The next scheduled race.</param>
        /// <param name="previousRace">The most recently completed race.</param>
        /// <returns>Information about the upcoming race weekend including time and status.</returns>
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

        /// <summary>
        /// Calculates the progress through the current or upcoming race weekend.
        /// </summary>
        /// <param name="nextRace">The next scheduled race.</param>
        /// <returns>Progress information for the race weekend including the next session and completion percentage.</returns>
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

        /// <summary>
        /// Retrieves season progress information including the previous race winner and race count.
        /// </summary>
        /// <param name="races">The complete list of races for the season.</param>
        /// <param name="previousRace">The most recently completed race.</param>
        /// <returns>Season progress information including previous race details and overall season progress.</returns>
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

        /// <summary>
        /// Retrieves and formats the current driver championship standings for overview display.
        /// </summary>
        /// <returns>Formatted driver standings including leader information and all positions with team colors.</returns>
        private async Task<OverviewDriverStandingsDTO> GetDriverStandings()
        {
            DriverStandingsDTO standings = await _driverService.GetCachedDriverStandings("current");
            DriverStandingDTO? firstPosition = standings.DriverStandings.FirstOrDefault(standing => standing.PositionText.Equals("1"));

            string leader = string.Empty;
            string color = string.Empty;
            if (firstPosition != null)
            {
                leader = firstPosition.GetDriverFullName();
                color = ConstructorColorConstants.GetColorForConstructor(firstPosition.Constructors.FirstOrDefault()?.Name);
            }

            List<DriverStandingDTO> sortedStandings = [.. standings.DriverStandings.OrderBy(x => int.Parse(x.PositionText))];
            List<OverviewDriverStandingsEntryDTO> finalStandingEntries = [.. sortedStandings.Select(entry => new OverviewDriverStandingsEntryDTO()
            {
                Driver = entry.GetDriverFullName(),
                Points = entry.Points,
                Position = entry.PositionText,
                Color = ConstructorColorConstants.GetColorForConstructor(entry.Constructors.FirstOrDefault()?.Name)
            })];

            OverviewDriverStandingsDTO finalStandings = new()
            {
                Leader = leader,
                Color = color,
                Standings = finalStandingEntries
            };

            return finalStandings;
        }

        /// <summary>
        /// Retrieves comprehensive overview data for the F1 dashboard.
        /// </summary>
        /// <returns>
        /// Complete overview data including race weekend progress, upcoming race information,
        /// season progress, and driver standings. Results are cached for 15 minutes.
        /// </returns>
        /// <exception cref="GeneralException">
        /// Thrown when no previous race is found (500 Internal Server Error) or when no upcoming race is found (404 Not Found).
        /// </exception>
        public async Task<OverviewDataDTO> GetOverviewData()
        {
            string key = CacheKeyConstants.OverviewData;
            string? result = await _cacheService.Get(key);

            if (result != null)
            {
                OverviewDataDTO? cachedOverviewData = JsonConvert.DeserializeObject<OverviewDataDTO>(result);
                if (cachedOverviewData != null)
                {
                    return cachedOverviewData;
                }
            }

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
            OverviewDriverStandingsDTO driverStandings = await GetDriverStandings();

            OverviewDataDTO overviewData = new()
            {
                RaceWeekendProgress = raceWeekendProgress,
                UpcomingRaceWeekend = upcomingRaceWeekend,
                SeasonProgress = seasonProgress,
                Drivers = driverStandings
            };

            await _cacheService.Add(key, JsonConvert.SerializeObject(overviewData), TimeSpan.FromMinutes(15));
            return overviewData;
        }
    }
}