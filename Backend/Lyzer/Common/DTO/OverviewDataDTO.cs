namespace Lyzer.Common.DTO
{
    public class OverviewDataDTO
    {
        public required RaceWeekendProgressDTO RaceWeekendProgress { get; set; }
    }

    public class RaceWeekendProgressDTO
    {
        public required string Name { get; set; }
        public required bool Ongoing { get; set; }
        public required int WeekendProgress { get; set; }
    }
}