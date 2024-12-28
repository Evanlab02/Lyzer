namespace Lyzer.Common.DTO
{
    public class ConstructorStandingsDTO
    {
        public string Season { get; set; }
        public string Round { get; set; }
        public List<ConstructorStandingDTO> ConstructorStandings { get; set; }
    }
    public class ConstructorStandingDTO
    {
        public string Position { get; set; }
        public string PositionText { get; set; }
        public string Points { get; set; }
        public string Wins { get; set; }
        public ConstructorDTO Constructor { get; set; }
    }
}
