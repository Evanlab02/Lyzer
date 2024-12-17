namespace Lyzer.Common.DTO
{
    public class DriverStandingsDTO
    {
        public required string Season { get; set; }
        public required string Round { get; set; }
        public required List<DriverStandingDTO> DriverStandings { get; set; }
    }

    public class DriverStandingDTO
    {
        public required string Position { get; set; }
        public required string PositionText { get; set; }
        public required string Points { get; set; }
        public required string Wins { get; set; }
        public required DriverDTO Driver { get; set; }
        public required List<ConstructorDTO> Constructors { get; set; }
    }
}