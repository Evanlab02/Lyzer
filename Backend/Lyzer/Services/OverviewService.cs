using Lyzer.Common.DTO;

namespace Lyzer.Services
{
    public class OverviewService
    {
        private readonly RacesService _racesService;

        public OverviewService(RacesService racesService) 
        {
            _racesService = racesService;
        }

        public async Task<OverviewDataDTO> GetOverviewData()
        {
            UpcomingRaceWeekendDTO upcomingRaceWeekend = await _racesService.GetUpcomingRaceWeekend();
            RaceWeekendProgressDTO raceWeekendProgress = await _racesService.GetRaceWeekendProgress();

            return new OverviewDataDTO
            {
                RaceWeekendProgress = raceWeekendProgress,
                UpcomingRaceWeekend = upcomingRaceWeekend,
            };
        }
    }
}
