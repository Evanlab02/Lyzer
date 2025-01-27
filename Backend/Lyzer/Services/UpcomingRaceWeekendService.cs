using Lyzer.Clients;
using Lyzer.Common.DTO;

using Newtonsoft.Json;

namespace Lyzer.Services
{
    public class UpcomingRaceWeekendService
    {
        private readonly RacesService _racesService;

        public UpcomingRaceWeekendService(RacesService racesService)
        {
            _racesService = racesService;
        }

        private RaceDTO? NextRace(RacesDTO racesDto)
        {
            var now = DateTimeOffset.UtcNow;

            return racesDto.Races
                .Where(r => r.RaceStartDateTime > now)
                .OrderBy(r => r.RaceStartDateTime)
                .FirstOrDefault();
        }

        public RaceDTO? LastRace(RacesDTO racesDto)
        {
            var now = DateTimeOffset.UtcNow;

            return racesDto.Races
                .Where(r => r.RaceStartDateTime < now)
                .OrderByDescending(r => r.RaceStartDateTime)
                .FirstOrDefault();
        }

        public DateTimeOffset NextFirstPractice(RacesDTO racesDto)
        {
            var nextRace = NextRace(racesDto) ?? throw new InvalidOperationException("No upcoming race found.");
            var fridayDate = nextRace.RaceStartDateTime.AddDays(-2);

            return fridayDate;
        }

        public bool IsRaceWeekend(RacesDTO racesDto)
        {
            var nextRace = NextRace(racesDto);
            if (nextRace == null)
            {
                return false;
            }

            DateTimeOffset raceDate = nextRace.RaceStartDateTime;
            DateTimeOffset practiceDate = NextFirstPractice(racesDto);
            var today = DateTimeOffset.UtcNow;

            return today >= practiceDate && today <= raceDate;
        }

        public double GetHoursUntilRaceWeekend(RacesDTO racesDto)
        {
            DateTimeOffset firstPractice = NextFirstPractice(racesDto).Date;

            var now = DateTimeOffset.UtcNow;
            TimeSpan diff = firstPractice - now;
            return diff.TotalHours < 0 ? 0 : diff.TotalHours;
        }

        public double TimeToRaceWeekendProgress(RacesDTO racesDto)
        {
            var lastRace = LastRace(racesDto);
            var nextRace = NextRace(racesDto);

            if (lastRace != null && nextRace != null)
            {
                TimeSpan timeDiff = nextRace.RaceStartDateTime - lastRace.RaceStartDateTime;
                double totaleHoursBetweenRaces = timeDiff.TotalHours;
                double timeSoFar = (DateTimeOffset.UtcNow - lastRace.RaceStartDateTime).TotalHours;

                double progress = timeSoFar / totaleHoursBetweenRaces;
                if (progress < 0) return 0;
                if (progress > 100) return 0;
                return progress;
            }
            return 0;
        }

        public string RaceWeekendProgressStatus(RacesDTO racesDto)
        {
            var raceWeekendProgress = TimeToRaceWeekendProgress(racesDto);

            if (raceWeekendProgress == 100)
            {
                return "It is race weekend!";
            }
            else
            {
                return raceWeekendProgress >= 80 ? "Almost" : "No";
            }
        }

        public async Task<UpcomingRaceWeekendDTO> GetUpcomingRaceWeekend(string season)
        {

            RacesDTO racesDto = await _racesService.GetCachedRaces(season);

            var isRaceWeekend = IsRaceWeekend(racesDto);
            var timeToRaceWeekendProgress = TimeToRaceWeekendProgress(racesDto);
            var status = RaceWeekendProgressStatus(racesDto);
            var timeToRaceWeekend = GetHoursUntilRaceWeekend(racesDto);

            return new UpcomingRaceWeekendDTO()
            {
                IsRaceWeekend = isRaceWeekend,
                TimeToRaceWeekendProgress = timeToRaceWeekendProgress,
                Status = status,
                TimeToRaceWeekend = timeToRaceWeekend
            };
        }

    }
}