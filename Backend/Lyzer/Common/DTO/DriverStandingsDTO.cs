namespace Lyzer.Common.DTO
{
    /// <summary>
    /// Represents the driver standings for a specific season and round.
    /// </summary>
    public class DriverStandingsDTO
    {
        /// <summary>
        /// The season year (e.g., "2024").
        /// </summary>
        public required string Season { get; set; }

        /// <summary>
        /// The round number in the season.
        /// </summary>
        public required string Round { get; set; }

        /// <summary>
        /// List of individual driver standings entries.
        /// </summary>
        public required List<DriverStandingDTO> DriverStandings { get; set; }
    }

    /// <summary>
    /// Represents a single driver's standing in the championship.
    /// </summary>
    public class DriverStandingDTO
    {
        /// <summary>
        /// The driver's position in the standings (numeric).
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// The driver's position as text (e.g., "1", "2", "3").
        /// </summary>
        public required string PositionText { get; set; }

        /// <summary>
        /// The total points accumulated by the driver.
        /// </summary>
        public required string Points { get; set; }

        /// <summary>
        /// The number of race wins by the driver.
        /// </summary>
        public required string Wins { get; set; }

        /// <summary>
        /// The driver's information.
        /// </summary>
        public required DriverDTO Driver { get; set; }

        /// <summary>
        /// List of constructors/teams the driver has raced for during the season.
        /// </summary>
        public required List<ConstructorDTO> Constructors { get; set; }

        /// <summary>
        /// Gets the driver's full name.
        /// </summary>
        /// <returns>The driver's full name.</returns>
        public string GetDriverFullName()
        {
            return Driver.GetFullName();
        }
    }
}