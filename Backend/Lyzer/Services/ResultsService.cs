using System.Runtime.Serialization;

using Lyzer.Clients;
using Lyzer.Common.Constants;
using Lyzer.Common.DTO;

using Newtonsoft.Json;

namespace Lyzer.Services
{
    public class ResultsService
    {
        private readonly ILogger<ResultsService> _logger;
        private readonly JolpicaClient _client;
        private readonly CacheService _cache;

        public ResultsService(ILogger<ResultsService> logger, JolpicaClient client, CacheService cache)
        {
            _logger = logger;
            _client = client;
            _cache = cache;
        }

        public async Task<ResultsDTO> GetCachedRaceResult(string year, string round)
        {
            string key = String.Format(CacheKeyConstants.Results, year, round);
            string? result = await _cache.Get(key);

            if (result == null)
            {
                ResultsDTO results = await _client.GetRaceResult(year, round);
                await _cache.Add(key, JsonConvert.SerializeObject(results), TimeSpan.FromHours(1));
                return results;
            }

            ResultsDTO? cachedResults = JsonConvert.DeserializeObject<ResultsDTO>(result);

            if (cachedResults == null)
            {
                throw new SerializationException("Could not deserialize cached result.");
            }

            return cachedResults;
        }
    }
}
