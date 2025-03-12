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
    }
}