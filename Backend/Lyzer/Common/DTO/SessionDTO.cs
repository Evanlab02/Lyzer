using System.Text.Json.Serialization;

namespace Lyzer.Common.DTO
{
    public class SessionDTO
    {
        public required string Name { get; set; }
        public required string Date { get; set; } // "2024-03-07"
        public required string Time { get; set; } // "13:30:00Z"


        [JsonIgnore]
        public DateTime SessionDateTime
        {
            get
            {
                DateTime localDateTime = DateTime.Parse($"{Date}T{Time}");
                return DateTime.SpecifyKind(localDateTime, DateTimeKind.Local).ToUniversalTime();
            }
        }
    }
}