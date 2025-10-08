namespace Lyzer.Common.Constants
{
    public static class JolpicaConstants
    {
        public static string BaseUri { get; } = "https://api.jolpi.ca/ergast/f1";
        public static string DriverStandingsUri { get; } = "/{0}/driverstandings";
        public static string ResultsUri { get; } = "/{0}/{1}/results";
        public static string ConstructorStandingsUri { get; } = "/{0}/constructorstandings";
        public static string RacesUri { get; } = "/{0}/races";
    }
}