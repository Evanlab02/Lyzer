using System;
using System.Runtime.Serialization;

using Lyzer.Clients;
using Lyzer.Common.Constants;
using Lyzer.Common.DTO;
using Lyzer.Common.Extensions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        private RaceDTO? GetNextOrCurrentRace(RacesDTO racesDto)
        {
            var now = DateTimeOffset.UtcNow;

            return racesDto.Races
                .Where(r => now < r.RaceStartDateTime.AddMinutes((int)MaxSessionTimeConstants.Race))
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
            var nextRace = GetNextOrCurrentRace(racesDto);

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
            var nextRace = GetNextOrCurrentRace(racesDto);
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
            var nextRace = GetNextOrCurrentRace(racesDto);

            if (nextRace != null)
            {
                // Change to start of year if not found
                DateTimeOffset fallbackDate = new(DateTime.UtcNow.Year, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
                DateTimeOffset lastRaceDate = lastRace != null
                    ? lastRace.RaceStartDateTime
                    : fallbackDate;

                DateTimeOffset nextRaceDate = nextRace.FirstPractice != null ? nextRace.FirstPractice.SessionDateTime : nextRace.RaceStartDateTime;

                TimeSpan timeDiff = nextRaceDate - lastRaceDate;
                double totalHoursBetweenRaces = timeDiff.TotalHours;
                double timeSoFar = (DateTimeOffset.UtcNow - lastRaceDate).TotalHours;

                double progress = timeSoFar / totalHoursBetweenRaces * 100;
                if (progress < 0) return 0;
                if (progress > 100) return 100;
                return (int)progress;
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

        public async Task<UpcomingRaceWeekendDTO> GetUpcomingRaceWeekend(string season = "current")
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
            var nextRace = GetNextOrCurrentRace(races);

            if (nextRace == null)
            {
                throw new InvalidOperationException("No upcoming race found.");
            }

            var nextSession = GetNextRaceSession(nextRace);

            var now = DateTimeOffset.UtcNow;

            if (nextSession == null
                    && (nextRace.RaceStartDateTime.AddMinutes((int)MaxSessionTimeConstants.Race) > now))
            {
                nextSession = new SessionDTO()
                {
                    Name = "Race",
                    Date = nextRace.RaceStartDateTime.Date.ToString(),
                    Time = nextRace.RaceStartDateTime.TimeOfDay.ToString()
                };
            }

            var isOngoing = nextSession?.SessionDateTime < now && nextSession.SessionDateTime.AddMinutes(GetMaxSessionTime(nextSession)) > now;
            var weekendProgressPercentage = GetWeekendProgressPercentage(nextRace, nextSession!);

            return new RaceWeekendProgressDTO()
            {
                Name = nextSession?.Name ?? "No upcoming session.",
                Ongoing = isOngoing,
                WeekendProgress = weekendProgressPercentage,
                StartDateTime = nextSession?.SessionDateTime
            };
        }

        private SessionDTO? GetNextRaceSession(RaceDTO race)
        {
            var now = DateTimeOffset.UtcNow;

            return race.Sessions.FirstOrDefault(x => x.SessionDateTime > now);
        }

        private int GetMaxSessionTime(SessionDTO session)
        {
            if (session.Name.Contains("Practice"))
                return (int)MaxSessionTimeConstants.Practice;

            var maxTime = Enums.GetValueFromEnumDescription<MaxSessionTimeConstants>(session.Name);

            if (maxTime == null)
                throw new Exception("Invalid session name provided");

            return maxTime.Value;
        }

        private int GetWeekendProgressPercentage(RaceDTO race, SessionDTO nextSession)
        {
            var now = DateTimeOffset.UtcNow;

            var remainingSessions = race.Sessions.Where(x => x.SessionDateTime <= now).Count();

            if (race.RaceStartDateTime < now)
                remainingSessions++;

            //+1 to include the race
            var totalSessions = race.Sessions.Count + 1;

            return (int)Math.Round((double)remainingSessions / totalSessions * 100);
        }
    }
}