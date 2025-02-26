using System.ComponentModel;

namespace Lyzer.Common.Constants
{
    public enum MaxSessionTimeConstants
    {
        [Description("Practice")]
        Practice = 60,
        [Description("Qualifying")]
        Qualifying = 60,
        [Description("Sprint Qualifying")]
        SprintQualifying = 44,
        [Description("Sprint")]
        Sprint = 45,
        [Description("Race")]
        Race = 180,
    }
}