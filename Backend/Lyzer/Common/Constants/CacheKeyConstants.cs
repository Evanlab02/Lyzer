namespace Lyzer.Common.Constants
{
    public static class CacheKeyConstants
    {
        public static string OverviewData { get; } = "lyzer-overview";
        public static string DriverStandings { get; } = "driver-standings-{0}";
        public static string Results { get; } = "results-{0}-{1}";
        public static string ConstructorStandings { get; } = "constructor-standings-{0}";
        public static string Races { get; } = "races-{0}";
    }
}