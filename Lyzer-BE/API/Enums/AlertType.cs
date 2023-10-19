namespace Lyzer_BE.API.Enums
{
    public enum AlertLevel
    {
        Critical = 0,
        NonCritical = 1
    }

    public class AlertChannel
    {
        //Somehow store this in a settings file for security
        public static string Critical = "lyzer-critical";
        public static string NonCritical = "lyzer-meh-maybelater";
    }
}
