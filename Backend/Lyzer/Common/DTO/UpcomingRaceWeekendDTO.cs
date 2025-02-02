namespace Lyzer.Common.DTO
{
    public class UpcomingRaceWeekendDTO
    {
        public required bool IsRaceWeekend { get; set; }
        public required int TimeToRaceWeekendProgress { get; set; }
        public required string Status { get; set; }
        public required double TimeToRaceWeekend { get; set; }
    }
}