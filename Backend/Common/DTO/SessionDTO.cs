using System.Text.Json.Serialization;

using Lyzer.Common.Constants;
using Lyzer.Common.Extensions;

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

        [JsonIgnore]
        public DateTime SessionEndDateTime
        {
            get
            {
                if (Name.Contains("Practice"))
                    return SessionDateTime.AddMinutes((int)MaxSessionTimeConstants.Practice);

                var maxTime = Enums.GetValueFromEnumDescription<MaxSessionTimeConstants>(Name);

                if (maxTime == null)
                    throw new Exception("Invalid session name provided");

                return SessionDateTime.AddMinutes(maxTime.Value);
            }
        }
    }
}