namespace Lyzer.Common.Constants
{
    /// <summary>
    /// Defines cache key constants used throughout the application for caching data.
    /// </summary>
    public static class CacheKeyConstants
    {
        /// <summary>
        /// Cache key for the overview data containing general F1 season information.
        /// </summary>
        public static string OverviewData { get; } = "lyzer-overview";

        /// <summary>
        /// Cache key template for driver standings data.
        /// Format: {0} = season year (e.g., "2024")
        /// </summary>
        public static string DriverStandings { get; } = "driver-standings-{0}";

        /// <summary>
        /// Cache key template for race results data.
        /// Format: {0} = season year, {1} = round number
        /// </summary>
        public static string Results { get; } = "results-{0}-{1}";

        /// <summary>
        /// Cache key template for constructor standings data.
        /// Format: {0} = season year (e.g., "2024")
        /// </summary>
        public static string ConstructorStandings { get; } = "constructor-standings-{0}";

        /// <summary>
        /// Cache key template for race schedule data.
        /// Format: {0} = season year (e.g., "2024")
        /// </summary>
        public static string Races { get; } = "races-{0}";
    }
}