using Lyzer.Common.Constants;
using Lyzer.Common.DTO;
using Lyzer.Errors;

namespace Lyzer.Common.Helpers
{
    public static class RacesHelper
    {
        public static RaceDTO? GetNextOrCurrentRace(RacesDTO racesDto)
        {
            var now = DateTimeOffset.UtcNow;

            return racesDto.Races
                .Where(r => now < r.RaceStartDateTime.AddMinutes((int)MaxSessionTimeConstants.Race))
                .OrderBy(r => r.RaceStartDateTime)
                .FirstOrDefault();
        }

        public static RaceDTO? GetPreviousRace(RacesDTO racesDto)
        {
            var now = DateTimeOffset.UtcNow;

            var previousRace = racesDto.Races
                .Where(r => r.RaceStartDateTime < now)
                .OrderByDescending(r => r.RaceStartDateTime)
                .FirstOrDefault();

            return previousRace;
        }

        public static SessionDTO GetNextRaceSession(RaceDTO race)
        {
            var now = DateTimeOffset.UtcNow;

            var nextSession = race.Sessions.FirstOrDefault(x => x.SessionDateTime > now);

            if (nextSession == null
                && (race.RaceStartDateTime.AddMinutes((int)MaxSessionTimeConstants.Race) > now))
            {
                nextSession = new SessionDTO()
                {
                    Name = "Race",
                    Date = race.RaceStartDateTime.Date.ToString(),
                    Time = race.RaceStartDateTime.TimeOfDay.ToString()
                };
            }

            return nextSession ?? throw new GeneralException("No upcoming session found.", StatusCodes.Status500InternalServerError);
        }

        public static int GetWeekendProgressPercentage(RaceDTO race)
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