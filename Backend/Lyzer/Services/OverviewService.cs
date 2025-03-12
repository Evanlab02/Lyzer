using Lyzer.Common.DTO;

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
            UpcomingRaceWeekendDTO upcomingRaceWeekend = await _racesService.GetUpcomingRaceWeekend();
            RaceWeekendProgressDTO raceWeekendProgress = await _racesService.GetRaceWeekendProgress();
            List<OverviewDriverDTO> drivers = await _driverService.GetCurrentDriverStandings();

            return new OverviewDataDTO
            {
                RaceWeekendProgress = raceWeekendProgress,
                UpcomingRaceWeekend = upcomingRaceWeekend,
                Drivers = drivers
            };
        }
    }
}