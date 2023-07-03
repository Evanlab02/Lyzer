using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Lyzer_BE.API.DTOs
{
    [ExcludeFromCodeCoverage]
    public class ScheduleDTO
    {
        [JsonPropertyName("MRData")]
        public ScheduleDataDTO ScheduleData { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ScheduleDataDTO
    {
        [JsonPropertyName("RaceTable")]
        public ScheduleTableDTO ScheduleTable { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ScheduleTableDTO
    {
        [JsonPropertyName("Races")]
        public List<RaceWeekendDTO> RaceWeekends { get; set; }
    }

    [ExcludeFromCodeCoverage]
    [BsonIgnoreExtraElements]
    public class RaceWeekendDTO
    {
        public string Round { get; set; }
        public string RaceName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public SessionDTO FirstPractice { get; set; }
        public SessionDTO SecondPractice { get; set; }
        public SessionDTO ThirdPractice { get; set; }
        public SessionDTO Qualifying { get; set; }
        public SessionDTO Sprint { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class SessionDTO
    {
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
