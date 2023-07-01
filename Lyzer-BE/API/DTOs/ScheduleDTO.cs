using System.Text.Json.Serialization;

namespace Lyzer_BE.API.DTOs
{
    public class ScheduleDTO
    {
        [JsonPropertyName("MRData")]
        public ScheduleDataDTO ScheduleData { get; set; }
    }

    public class ScheduleDataDTO
    {
        [JsonPropertyName("RaceTable")]
        public ScheduleTableDTO ScheduleTable { get; set; }
    }

    public class ScheduleTableDTO
    {
        [JsonPropertyName("Races")]
        public List<RaceWeekendDTO> RaceWeekends { get; set; }
    }

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

    public class SessionDTO
    {
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
