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
        public string? Time { get; set; }
        // Sub-objects for each session
        public SessionDTO? FirstPractice { get; set; }
        public SessionDTO? SecondPractice { get; set; }
        public SessionDTO? ThirdPractice { get; set; }
        public SessionDTO? Qualifying { get; set; }
        public SessionDTO? Sprint {  get; set; }
        public SessionDTO? SprintQualifying { get; set; }
        public required List<SessionDTO> Sessions {  get; set; }

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