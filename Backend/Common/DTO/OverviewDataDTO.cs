namespace Lyzer.Common.DTO
{
    /// <summary>
    /// Represents the overview data for the F1 dashboard, containing race weekend progress, upcoming races, season progress, and driver standings.
    /// </summary>
    public class OverviewDataDTO
    {
        /// <summary>
        /// Progress information for the current or next race weekend.
        /// </summary>
        public required RaceWeekendProgressDTO RaceWeekendProgress { get; set; }

        /// <summary>
        /// Information about the upcoming race weekend.
        /// </summary>
        public required UpcomingRaceWeekendDTO UpcomingRaceWeekend { get; set; }

        /// <summary>
        /// Current season progress information.
        /// </summary>
        public required SeasonProgressDTO SeasonProgress { get; set; }

        /// <summary>
        /// Driver championship standings overview.
        /// </summary>
        public required OverviewDriverStandingsDTO Drivers { get; set; }
    }

    /// <summary>
    /// Represents the progress of the current race weekend.
    /// </summary>
    public class RaceWeekendProgressDTO
    {
        /// <summary>
        /// Name of the current or next session (e.g., "Practice 1", "Qualifying", "Race").
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Indicates whether a session is currently ongoing.
        /// </summary>
        public required bool Ongoing { get; set; }

        /// <summary>
        /// Progress through the race weekend as a percentage (0-100).
        /// </summary>
        public required int WeekendProgress { get; set; }

        /// <summary>
        /// Start date and time of the next session.
        /// </summary>
        public DateTime? StartDateTime { get; set; }
    }

    /// <summary>
    /// Represents information about the upcoming race weekend.
    /// </summary>
    public class UpcomingRaceWeekendDTO
    {
        /// <summary>
        /// Indicates whether it is currently race weekend.
        /// </summary>
        public required bool IsRaceWeekend { get; set; }

        /// <summary>
        /// Progress towards the next race weekend as a percentage (0-100).
        /// </summary>
        public required int TimeToRaceWeekendProgress { get; set; }

        /// <summary>
        /// Status message indicating how soon the race weekend is (e.g., "No", "Almost", "It is race weekend!").
        /// </summary>
        public required string Status { get; set; }

        /// <summary>
        /// Time remaining until the race weekend starts, in minutes.
        /// </summary>
        public required double TimeToRaceWeekend { get; set; }
    }

    /// <summary>
    /// Represents the current season's progress and recent results.
    /// </summary>
    public class SeasonProgressDTO
    {
        /// <summary>
        /// Full name of the winner of the previous race.
        /// </summary>
        public required string PreviousRaceWinner { get; set; }

        /// <summary>
        /// Name of the previous Grand Prix.
        /// </summary>
        public required string PreviousGrandPrix { get; set; }

        /// <summary>
        /// Number of races completed in the current season.
        /// </summary>
        public required int SeasonProgress { get; set; }

        /// <summary>
        /// Total number of races scheduled for the season.
        /// </summary>
        public required int SeasonTotalRaces { get; set; }
    }

    /// <summary>
    /// Represents a summary of the driver championship standings.
    /// </summary>
    public class OverviewDriverStandingsDTO
    {
        /// <summary>
        /// Full name of the championship leader.
        /// </summary>
        public required string Leader { get; set; }

        /// <summary>
        /// Hex color code of the leader's team.
        /// </summary>
        public required string Color { get; set; }

        /// <summary>
        /// List of driver standings entries.
        /// </summary>
        public required List<OverviewDriverStandingsEntryDTO> Standings { get; set; }
    }

    /// <summary>
    /// Represents a single entry in the overview driver standings.
    /// </summary>
    public class OverviewDriverStandingsEntryDTO
    {
        /// <summary>
        /// The driver's position in the championship.
        /// </summary>
        public required string Position { get; set; }

        /// <summary>
        /// The driver's full name.
        /// </summary>
        public required string Driver { get; set; }

        /// <summary>
        /// The driver's total championship points.
        /// </summary>
        public required string Points { get; set; }

        /// <summary>
        /// Hex color code of the driver's team.
        /// </summary>
        public required string Color { get; set; }
    }
}