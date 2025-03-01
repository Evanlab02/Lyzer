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
                throw new Exception404NotFound("Could not retrieve data at: " + requestPath);
            }

            JsonElement root = result.RootElement;

            JsonElement standings = root
                .GetProperty("MRData")
                .GetProperty("StandingsTable")
                .GetProperty("StandingsLists")[0];

            DriverStandingsDTO? driverStandings = JsonConvert.DeserializeObject<DriverStandingsDTO>(standings.GetRawText());

            if (driverStandings == null)
            {
                throw new SerializationException("Could not deserialize driver standings.");
            }

            return driverStandings;
        }

        public async Task<ResultsDTO> GetRaceResult(string year, string round)
        {
            string requestPath = String.Format(JolpicaConstants.ResultsUri, year, round);
            JsonDocument? result = await _client.GetAsync<JsonDocument>(requestPath);

            if (result == null)
                throw new Exception404NotFound("Could not retrieve data at: " + requestPath);

            JsonElement root = result.RootElement;

            JsonElement results = root
                .GetProperty("MRData")
                .GetProperty("RaceTable")
                .GetProperty("Races")[0];

            ResultsDTO? latestResults = JsonConvert.DeserializeObject<ResultsDTO>(results.GetRawText());

            if (latestResults == null)
                throw new SerializationException("Could not deserialize race results.");

            return latestResults;
        }

        public async Task<ConstructorStandingsDTO> GetContructorsStandingsForYear(string year)
        {
            string requestPath = String.Format(JolpicaConstants.ConstructorStandingsUri, year);
            JsonDocument? result = await _client.GetAsync<JsonDocument>(requestPath);

            if (result == null)
                throw new Exception404NotFound("Could not retrieve data at: " + requestPath);

            JsonElement root = result.RootElement;

            JsonElement standings = root
                .GetProperty("MRData")
                .GetProperty("StandingsTable")
                .GetProperty("StandingsLists")[0];

            ConstructorStandingsDTO? constructorStandings = JsonConvert.DeserializeObject<ConstructorStandingsDTO>(standings.GetRawText());

            if (constructorStandings == null)
                throw new SerializationException($"Could not deserialize constructor standings for year {year}");

            return constructorStandings;
        }
        public async Task<RacesDTO> GetAllRacesForSeason(string season)
        {
            string requestPath = String.Format(JolpicaConstants.RacesUri, season);
            JsonDocument? result = await _client.GetAsync<JsonDocument>(requestPath);

            if (result == null)
            {
                throw new Exception404NotFound("Could not retrieve data at: " + requestPath);
            }

            JsonElement root = result.RootElement;

            JsonElement races = root
                .GetProperty("MRData")
                .GetProperty("RaceTable");

            RacesDTO? raceList = JsonConvert.DeserializeObject<RacesDTO>(races.GetRawText());

            if (raceList == null)
            {
                throw new SerializationException("Could not deserialize race results.");
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