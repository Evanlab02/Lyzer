using System.Text.Json.Serialization;

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

        // Actual race date/time 
        public required string Date { get; set; }
        public required string Time { get; set; }
        // Sub-objects for each session
        public required SessionDTO FirstPractice { get; set; }
        public required SessionDTO SecondPractice { get; set; }
        public required SessionDTO ThirdPractice { get; set; }
        public required SessionDTO Qualifying { get; set; }

        // Computed property for the actual race's date/time
        [JsonIgnore]
        public DateTimeOffset RaceStartDateTime
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Time))
                {
                    return DateTimeOffset.Parse(Date);
                }
                {
                    return DateTimeOffset.Parse($"{Date}T{Time}");
                }
            }
        }
    }
}