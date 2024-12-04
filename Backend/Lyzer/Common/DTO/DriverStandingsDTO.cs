namespace Lyzer.Common.DTO
{
    public class DriverStandingsDTO
    {
        public string Season { get; set; }
        public string Round { get; set; }
        public List<DriverStandingDTO> DriverStandings { get; set; }
    }

    public class DriverStandingDTO
    {
        public string Position { get; set; }
        public string PositionText { get; set; }
        public string Points { get; set; }
        public string Wins { get; set; }
        public DriverDTO Driver { get; set; }
        public List<ConstructorDTO> Constructors { get; set; }
    }
}
