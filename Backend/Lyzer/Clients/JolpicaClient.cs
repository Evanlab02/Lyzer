using System.Runtime.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using RestSharp;
using Lyzer.Common.Constants;
using Lyzer.Common.DTO;
using Lyzer.Errors;

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

        public async Task<DriverStandingsDTO> GetCurrentDriverStandings()
        {
            string requestPath = String.Format(JolpicaConstants.DriverStandingsUri, "current");
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

            return raceList;
        }
    }
}
