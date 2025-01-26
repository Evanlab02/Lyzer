namespace Lyzer.Common.Constants
{
    public static class JolpicaConstants
    {
        public static string BaseUri { get; set; } = "https://api.jolpi.ca/ergast/f1";
        public static string DriverStandingsUri { get; set; } = "/{0}/driverstandings";
        public static string ResultsUri { get; set; } = "/{0}/{1}/results";
        public static string ConstructorStandingsUri { get; set; } = "/{0}/constructorstandings";
        public static string RacesUri { get; set; } = "/{0}/races";
        public static string UpcomingRaceWeekendUri { get; set; } = "/{0}/upcomingraceweekend";
    }
}