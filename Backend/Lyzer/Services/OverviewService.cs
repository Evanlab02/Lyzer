using Lyzer.Common.DTO;
using Lyzer.Common.Helpers;
using Lyzer.Errors;

namespace Lyzer.Services
{
    public class OverviewService
    {
        private readonly RacesService _racesService;
        private readonly DriverService _driverService;

        public OverviewService(RacesService racesService, DriverService driverService)
        {
            _racesService = racesService;
            _driverService = driverService;
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

            if (nextRace == null)
            {
                throw new GeneralException("No upcoming race.", StatusCodes.Status404NotFound);
            }

            if (previousRace == null)
            {
                throw new GeneralException("No previous race found.", StatusCodes.Status500InternalServerError);
            }

            UpcomingRaceWeekendDTO upcomingRaceWeekend = _racesService.GetUpcomingRaceWeekend(nextRace, previousRace);
            RaceWeekendProgressDTO raceWeekendProgress = _racesService.GetRaceWeekendProgress(nextRace);

            return new OverviewDataDTO
            {
                RaceWeekendProgress = raceWeekendProgress,
                UpcomingRaceWeekend = upcomingRaceWeekend,
                Drivers = drivers
            };
        }
    }
}