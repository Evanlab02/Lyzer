using System.Runtime.Serialization;

using Lyzer.Clients;
using Lyzer.Common.Constants;
using Lyzer.Common.DTO;

using Newtonsoft.Json;

namespace Lyzer.Services
{
    public class RacesService
    {
        private readonly ILogger<RacesService> _logger;
        private readonly JolpicaClient _client;
        private readonly CacheService _cache;

        public RacesService(ILogger<RacesService> logger, JolpicaClient client, CacheService cache)
        {
            _logger = logger;
            _client = client;
            _cache = cache;
        }

        public async Task<RacesDTO> GetCachedRaces(string season)
        {
            string key = String.Format(CacheKeyConstants.Races, season);
            string? race = await _cache.Get(key);

            if (race == null)
            {
                RacesDTO races = await _client.GetAllRacesForSeason(season);
                await _cache.Add(key, JsonConvert.SerializeObject(races), TimeSpan.FromHours(24));
                return races;
            }

            RacesDTO? cachedRaces = JsonConvert.DeserializeObject<RacesDTO>(race);

            if (cachedRaces == null)
            {
                throw new SerializationException("Could not deserialize cached races.");
            }

            return cachedRaces;
        }

        private RaceDTO? GetNextRace(RacesDTO racesDto)
        {
            var now = DateTimeOffset.UtcNow;

            return racesDto.Races
                .Where(r => r.RaceStartDateTime > now)
                .OrderBy(r => r.RaceStartDateTime)
                .FirstOrDefault();
        }

        public RaceDTO? GetLastRace(RacesDTO racesDto)
        {
            var now = DateTimeOffset.UtcNow;

            return racesDto.Races
                .Where(r => r.RaceStartDateTime < now)
                .OrderByDescending(r => r.RaceStartDateTime)
                .FirstOrDefault();
        }

        public DateTimeOffset NextFirstSession(RacesDTO racesDto)
        {
            var nextRace = GetNextRace(racesDto);

            if (nextRace == null)
            {
                throw new InvalidOperationException("No upcoming race found.");
            }

            var firstSession = nextRace.Sessions.FirstOrDefault();

            if (firstSession == null)
            {
                return nextRace.RaceStartDateTime.AddDays(-2);
            }

            return firstSession.SessionDateTime;
        }

        public bool IsRaceWeekend(RacesDTO racesDto)
        {
            var nextRace = GetNextRace(racesDto);
            if (nextRace == null)
            {
                return false;
            }

            DateTimeOffset raceDate = nextRace.RaceStartDateTime;
            DateTimeOffset practiceDate = NextFirstSession(racesDto);
            var today = DateTimeOffset.UtcNow;

            return today >= practiceDate && today <= raceDate;
        }

        public int GetMinutesToRaceWeekend(RacesDTO racesDto)
        {
            DateTimeOffset firstPractice = NextFirstSession(racesDto).Date;

            var now = DateTimeOffset.UtcNow;
            TimeSpan diff = firstPractice - now;
            return (int)(diff.TotalMinutes < 0 ? 0 : diff.TotalMinutes);
        }

        public int TimeToRaceWeekendProgress(RacesDTO racesDto)
        {
            var lastRace = GetLastRace(racesDto);
            var nextRace = GetNextRace(racesDto);

            if (lastRace != null && nextRace != null)
            {
                TimeSpan timeDiff = nextRace.RaceStartDateTime - lastRace.RaceStartDateTime;
                int totalHoursBetweenRaces = (int)timeDiff.TotalHours;
                int timeSoFar = (int)(DateTimeOffset.UtcNow - lastRace.RaceStartDateTime).TotalHours;

                int progress = timeSoFar / totalHoursBetweenRaces * 100;
                if (progress < 0) return 0;
                if (progress > 100) return 0;
                return progress;
            }
            return 0;
        }

        public string UpcomingRaceWeekendProgressStatus(RacesDTO racesDto)
        {
            var raceWeekendProgress = TimeToRaceWeekendProgress(racesDto);

            switch (raceWeekendProgress)
            {
                case 100:
                    return "It is race weekend!";
                case int progress when progress >= 80:
                    return "Almost";
                default:
                    return "No";
            }
        }

        public async Task<UpcomingRaceWeekendDTO> GetUpcomingRaceWeekend(string season)
        {
            RacesDTO racesDto = await GetCachedRaces(season);

            var isRaceWeekend = IsRaceWeekend(racesDto);
            var timeToRaceWeekendProgress = TimeToRaceWeekendProgress(racesDto);
            var status = UpcomingRaceWeekendProgressStatus(racesDto);
            var timeToRaceWeekend = GetMinutesToRaceWeekend(racesDto);

            return new UpcomingRaceWeekendDTO()
            {
                IsRaceWeekend = isRaceWeekend,
                TimeToRaceWeekendProgress = timeToRaceWeekendProgress,
                Status = status,
                TimeToRaceWeekend = timeToRaceWeekend
            };
        }

        public async Task<RaceWeekendProgressDTO> GetRaceWeekendProgress()
        {
            var races = await GetCachedRaces("current");
            var nextRace = GetNextRace(races);

            if (nextRace == null)
            {
                throw new InvalidOperationException("No upcoming race found.");
            }

            var nextSession = GetNextRaceSession(nextRace);

            var now = DateTimeOffset.UtcNow;

            if (nextSession == null && (nextRace.RaceStartDateTime))

            return new RaceWeekendProgressDTO() 
            {
                Name = nextSession.Name,
                Ongoing = false,
                WeekendProgress = 0,
            };
        }

        private SessionDTO? GetNextRaceSession(RaceDTO race)
        {
            var now = DateTimeOffset.UtcNow;

            return race.Sessions.FirstOrDefault(x => x.SessionDateTime > now);
        }
    }
}