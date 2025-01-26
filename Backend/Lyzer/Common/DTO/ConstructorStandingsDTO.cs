namespace Lyzer.Common.DTO
{
    public class ConstructorStandingsDTO
    {
        public required string Season { get; set; }
        public required string Round { get; set; }
        public required List<ConstructorStandingDTO> ConstructorStandings { get; set; }
    }
    public class ConstructorStandingDTO
    {
        public string? Position { get; set; }
        public required string PositionText { get; set; }
        public required string Points { get; set; }
        public required string Wins { get; set; }
        public required ConstructorDTO Constructor { get; set; }
    }
}