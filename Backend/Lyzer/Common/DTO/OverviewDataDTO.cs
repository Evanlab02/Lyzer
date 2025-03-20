namespace Lyzer.Common.DTO
{
    public class OverviewDataDTO
    {
        public required RaceWeekendProgressDTO RaceWeekendProgress { get; set; }
        public required UpcomingRaceWeekendDTO UpcomingRaceWeekend { get; set; }
        public required SeasonProgressDTO SeasonProgress { get; set; }
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

    public class SeasonProgressDTO
    {
        public required string PreviousRaceWinner { get; set; }
        public required string PreviousGrandPrix { get; set; }
        public required int SeasonProgress { get; set; }
        public required int SeasonTotalRaces { get; set; }
    }
}