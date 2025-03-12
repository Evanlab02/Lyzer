using System;
using System.Runtime.Serialization;

using Lyzer.Clients;
using Lyzer.Common.Constants;
using Lyzer.Common.DTO;
using Lyzer.Common.Extensions;
using Lyzer.Common.Helpers;
using Lyzer.Errors;

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

        public UpcomingRaceWeekendDTO GetUpcomingRaceWeekend(RaceDTO nextRace, RaceDTO previousRace)
        {
            var firstSession = nextRace.Sessions.FirstOrDefault();
            DateTimeOffset firstSessionDateTime = nextRace.RaceStartDateTime.AddDays(-2);

            if (firstSession != null)
            {
                firstSessionDateTime = firstSession.SessionDateTime;
            }

            var isRaceWeekend = DateTimeHelper.IsOngoing(firstSessionDateTime.DateTime, nextRace.RaceStartDateTime.DateTime);
            var timeToRaceWeekendProgress = TimeToRaceWeekendProgress(nextRace, previousRace);
            var timeToRaceWeekend = GetMinutesToRaceWeekend(firstSessionDateTime);

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

        private int TimeToRaceWeekendProgress(RaceDTO nextRace, RaceDTO previousRace)
        {
            DateTimeOffset lastRaceDate = previousRace.RaceStartDateTime;

            DateTimeOffset nextRaceDate = nextRace.FirstPractice != null ? nextRace.FirstPractice.SessionDateTime : nextRace.RaceStartDateTime;

            TimeSpan timeDiff = nextRaceDate - lastRaceDate;
            double totalHoursBetweenRaces = timeDiff.TotalHours;
            double timeSoFar = (DateTimeOffset.UtcNow - lastRaceDate).TotalHours;

            double progress = timeSoFar / totalHoursBetweenRaces * 100;
            if (progress < 0) return 0;
            if (progress > 100) return 100;
            return (int)progress;
        }

        private int GetMinutesToRaceWeekend(DateTimeOffset firstSession)
        {
            var now = DateTimeOffset.UtcNow;
            TimeSpan diff = firstSession - now;
            return (int)(diff.TotalMinutes < 0 ? 0 : diff.TotalMinutes);
        }

        public RaceWeekendProgressDTO GetRaceWeekendProgress(RaceDTO nextRace)
        {
            var nextSession = RacesHelper.GetNextRaceSession(nextRace);

            var now = DateTimeOffset.UtcNow;

            var isOngoing = DateTimeHelper.IsOngoing(nextSession.SessionDateTime, nextSession.SessionEndDateTime);
            var weekendProgressPercentage = GetWeekendProgressPercentage(nextRace, nextSession);

            return new RaceWeekendProgressDTO()
            {
                Name = nextSession.Name,
                Ongoing = isOngoing,
                WeekendProgress = weekendProgressPercentage,
                StartDateTime = nextSession?.SessionDateTime
            };
        }

        private int GetWeekendProgressPercentage(RaceDTO race, SessionDTO nextSession)
        {
            var now = DateTimeOffset.UtcNow;

            var completedSessions = race.Sessions.Where(x => x.SessionDateTime <= now).Count();

            if (race.RaceStartDateTime < now)
                completedSessions++;

            //+1 to include the race
            var totalSessions = race.Sessions.Count + 1;

            return (int)Math.Round((double)completedSessions / totalSessions * 100);
        }
    }
}