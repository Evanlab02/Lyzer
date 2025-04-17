using Lyzer.Common.DTO;
using Lyzer.Common.Helpers;
using Lyzer.Errors;

namespace Lyzer.Services
{
    public class OverviewService
    {
        private readonly RacesService _racesService;
        private readonly ResultsService _resultsService;

        public OverviewService(RacesService racesService, ResultsService resultsService)
        {
            _racesService = racesService;
            _resultsService = resultsService;
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

            var nextRace = RacesHelper.GetNextOrCurrentRace(races);
            var previousRace = RacesHelper.GetPreviousRace(races);

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

            UpcomingRaceWeekendDTO upcomingRaceWeekend = _racesService.GetUpcomingRaceWeekend(nextRace, previousRace);
            RaceWeekendProgressDTO raceWeekendProgress = _racesService.GetRaceWeekendProgress(nextRace);
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