namespace Lyzer.Common.Constants
{
    public static class JolpicaConstants
    {
        public static string BaseUri { get; set; } = "https://api.jolpi.ca/ergast/f1";
        public static string DriverStandingsUri { get; set; } = "/{0}/driverstandings";
        public static string ResultsUri { get; set; } = "/{0}/{1}/results";
    }
}
