using Lyzer.Common.DTO;

namespace Lyzer.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static bool IsOngoing(DateTime start, DateTime end)
        {
            var now = DateTime.Now;
            return now >= start && now <= end;
        }

        public static int GetMinutesUntilDateTimeOffset(DateTimeOffset time)
        {
            var now = DateTimeOffset.UtcNow;
            TimeSpan diff = time - now;
            return (int)(diff.TotalMinutes < 0 ? 0 : diff.TotalMinutes);
        }

        public static int GetTimeProgressBetweenDateTimeOffsetsAsPercentage(DateTimeOffset previousDateTime, DateTimeOffset nextDateTime)
        {
            // DateTimeOffset lastRaceDate = previousRace.RaceStartDateTime;
            // DateTimeOffset nextRaceDate = nextRace.FirstPractice != null ? nextRace.FirstPractice.SessionDateTime : nextRace.RaceStartDateTime;

            TimeSpan timeDiff = previousDateTime - nextDateTime;
            double totalHoursBetweenRaces = timeDiff.TotalHours;
            double timeSoFar = (DateTimeOffset.UtcNow - previousDateTime).TotalHours;

            double progress = timeSoFar / totalHoursBetweenRaces * 100;
            if (progress < 0) return 0;
            if (progress > 100) return 100;
            return (int)progress;
        }
    }
}