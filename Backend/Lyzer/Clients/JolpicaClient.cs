using System.Runtime.Serialization;
using System.Text.Json;

using Lyzer.Common.Constants;
using Lyzer.Common.DTO;
using Lyzer.Errors;

using Newtonsoft.Json;

using RestSharp;

namespace Lyzer.Clients
{
    public class JolpicaClient
    {
        private readonly ILogger<JolpicaClient> _logger;
        private readonly RestClient _client;
        private const string _ResponseWrapper = "MRData";

        public JolpicaClient(ILogger<JolpicaClient> logger)
        {
            RestClientOptions options = new RestClientOptions(JolpicaConstants.BaseUri);
            _client = new RestClient(options);
            _logger = logger;
        }

        public async Task<DriverStandingsDTO> GetDriverStandings(string year)
        {
            string requestPath = String.Format(JolpicaConstants.DriverStandingsUri, year);
            JsonDocument? result = await _client.GetAsync<JsonDocument>(requestPath);

            if (result == null)
            {
                _logger.LogError("Could not retrieve driver standings from Jolpica for {Year}", year);
                throw new CustomHttpException("Could not retrieve driver standings from Jolpica for " + year, StatusCodes.Status500InternalServerError);
            }

            JsonElement root = result.RootElement;

            JsonElement standingsLists = root
                .GetProperty(_ResponseWrapper)
                .GetProperty("StandingsTable")
                .GetProperty("StandingsLists");

            if (standingsLists.GetArrayLength() == 0)
            {
                _logger.LogError("No standings found for {Year}", year);
                throw new CustomHttpException("No standings found for " + year, StatusCodes.Status404NotFound);
            }

            JsonElement standings = standingsLists[0];
            DriverStandingsDTO? driverStandings = JsonConvert.DeserializeObject<DriverStandingsDTO>(standings.GetRawText());

            if (driverStandings == null)
            {
                _logger.LogError("Could not deserialize driver standings for {Year}", year);
                throw new SerializationException("Could not deserialize driver standings for " + year);
            }

            return driverStandings;
        }

        public async Task<ResultsDTO> GetRaceResult(string year, string round)
        {
            string requestPath = String.Format(JolpicaConstants.ResultsUri, year, round);
            JsonDocument? result = await _client.GetAsync<JsonDocument>(requestPath);

            if (result == null)
            {
                _logger.LogError("Could not retrieve results from jolpica for year: {Year} and round: {Round}", year, round);
                throw new CustomHttpException("Could not retrieve results from jolpica for year: " + year + " and round: " + round, StatusCodes.Status500InternalServerError);
            }

            JsonElement root = result.RootElement;

            JsonElement resultsLists = root
                .GetProperty(_ResponseWrapper)
                .GetProperty("RaceTable")
                .GetProperty("Races");

            if (resultsLists.GetArrayLength() == 0)
            {
                _logger.LogError("No results found for year: {Year} and round: {Round}", year, round);
                throw new CustomHttpException("No results found for year: " + year + " and round: " + round, StatusCodes.Status404NotFound);
            }

            JsonElement results = resultsLists[0];
            ResultsDTO? latestResults = JsonConvert.DeserializeObject<ResultsDTO>(results.GetRawText());

            if (latestResults == null)
            {
                _logger.LogError("Could not deserialize race results for year: {Year} and round: {Round}", year, round);
                throw new SerializationException("Could not deserialize race results for year: " + year + " and round: " + round);
            }

            return latestResults;
        }

        public async Task<ConstructorStandingsDTO> GetContructorsStandingsForYear(string year)
        {
            string requestPath = String.Format(JolpicaConstants.ConstructorStandingsUri, year);
            JsonDocument? result = await _client.GetAsync<JsonDocument>(requestPath);

            if (result == null)
            {
                _logger.LogError("Could not retrieve constructor standings from jolpica for year: {Year}", year);
                throw new CustomHttpException("Could not retrieve constructor standings from jolpica for year: " + year, StatusCodes.Status500InternalServerError);
            }

            JsonElement root = result.RootElement;

            JsonElement standingsLists = root
                .GetProperty(_ResponseWrapper)
                .GetProperty("StandingsTable")
                .GetProperty("StandingsLists");

            if (standingsLists.GetArrayLength() == 0)
            {
                _logger.LogError("No constructor standings found for year: {Year}", year);
                throw new CustomHttpException("No constructor standings found for year: " + year, StatusCodes.Status404NotFound);
            }

            JsonElement standings = standingsLists[0];
            ConstructorStandingsDTO? constructorStandings = JsonConvert.DeserializeObject<ConstructorStandingsDTO>(standings.GetRawText());

            if (constructorStandings == null)
            {
                _logger.LogError("Could not deserialize constructor standings for year: {Year}", year);
                throw new SerializationException("Could not deserialize constructor standings for year: " + year);
            }

            return constructorStandings;
        }
        public async Task<RacesDTO> GetAllRacesForSeason(string season)
        {
            string requestPath = String.Format(JolpicaConstants.RacesUri, season);
            JsonDocument? result = await _client.GetAsync<JsonDocument>(requestPath);

            if (result == null)
            {
                _logger.LogError("Could not retrieve races from jolpica for year: {Year}", season);
                throw new CustomHttpException("Could not retrieve races from jolpica for year: " + season, StatusCodes.Status500InternalServerError);
            }

            JsonElement root = result.RootElement;

            JsonElement races = root
                .GetProperty(_ResponseWrapper)
                .GetProperty("RaceTable");

            RacesDTO? raceList = JsonConvert.DeserializeObject<RacesDTO>(races.GetRawText());

            if (raceList == null)
            {
                _logger.LogError("Could not deserialize races for year: {Year}", season);
                throw new SerializationException("Could not deserialize races for year: " + season);
            }

            foreach (RaceDTO race in raceList.Races)
            {
                List<SessionDTO?> allSessions = new List<SessionDTO?> { race.FirstPractice, race.SecondPractice, race.ThirdPractice, race.Qualifying, race.Sprint, race.SprintQualifying };

                /*
                 * 1. Creates an array of tuples, where item 1 is the SessionDTO? and item 2 is a string of the human readable session name
                 * 2. Filters out the null sessions, as most races don't have all the sessions
                 * 3. Converts it to a KeyValuePair/Dictionary, where we can get the session name by reference of the object
                 */

                var sessionNames = new Dictionary<SessionDTO, string>(
                    new[]
                    {
                        (race.FirstPractice, "First Practice"),
                        (race.SecondPractice, "Second Practice"),
                        (race.ThirdPractice, "Third Practice"),
                        (race.Qualifying, "Qualifying"),
                        (race.Sprint, "Sprint"),
                        (race.SprintQualifying, "Sprint Qualifying")
                    }
                    .Where(x => x.Item1 != null)
                    .Select(x => new KeyValuePair<SessionDTO, string>(x.Item1!, x.Item2))
                );

                /*
                 * 1. Filters out all the null sessions
                 * 2. Sets the session name, via the dictionary we created above.
                 */

                race.Sessions = allSessions
                    .Where(x => x != null)
                    .Select(session =>
                        {
                            session!.Name = sessionNames[session];
                            return session;
                        })
                    .ToList();
            }

            return raceList;
        }
    }
}