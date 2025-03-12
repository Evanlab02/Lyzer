namespace Lyzer.Common.DTO
{
    public class OverviewDataDTO
    {
        public required RaceWeekendProgressDTO RaceWeekendProgress { get; set; }
        public required UpcomingRaceWeekendDTO UpcomingRaceWeekend { get; set; }
        public required List<OverviewDriverDTO> Drivers { get; set; }
    }

    public class RaceWeekendProgressDTO
    {
        public required string Name { get; set; }
        public required bool Ongoing { get; set; }
        public required int WeekendProgress { get; set; }
        public DateTime? StartDateTime { get; set; }
    }

    public class UpcomingRaceWeekendDTO
    {
        public required bool IsRaceWeekend { get; set; }
        public required int TimeToRaceWeekendProgress { get; set; }
        public required string Status { get; set; }
        public required double TimeToRaceWeekend { get; set; }
    }

    public class OverviewDriverDTO
    {
        public required string Name { get; set; }
        public required int Position { get; set; }
        public required int Points { get; set; }
        public required string Colour { get; set; }
    }
}