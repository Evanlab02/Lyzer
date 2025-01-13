namespace Lyzer.Common.DTO
{
    public class RacesDTO
    {
        public required string Season { get; set; }
        public required List<RaceDTO> Races { get; set; }
    }
    public class RaceDTO
    {
        public required string Season { get; set; }
        public required string Round { get; set; }
        public required string Url { get; set; }
        public required string RaceName { get; set; }
        public required CircuitDTO Circuit { get; set; }
        public required string Date { get; set; }
        public required string Time { get; set; }
    }
}