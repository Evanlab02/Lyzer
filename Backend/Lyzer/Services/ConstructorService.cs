using System.Runtime.Serialization;

using Lyzer.Clients;
using Lyzer.Common.Constants;
using Lyzer.Common.DTO;

using Newtonsoft.Json;

namespace Lyzer.Services
{
    public class ConstructorService
    {
        private readonly ILogger<ConstructorService> _logger;
        private readonly CacheService _cache;
        private readonly JolpicaClient _client;

        public ConstructorService(ILogger<ConstructorService> logger, CacheService cache, JolpicaClient client)
        {
            _logger = logger;
            _cache = cache;
            _client = client;
        }

        public async Task<ConstructorStandingsDTO> GetCachedConstructorStandings(string year)
        {
            string key = String.Format(CacheKeyConstants.ConstructorStandings, year);
            string? result = await _cache.Get(key);

            if (result == null)
            {
                ConstructorStandingsDTO standings = await _client.GetContructorsStandingsForYear(year);
                await _cache.Add(key, JsonConvert.SerializeObject(standings), TimeSpan.FromHours(1));
                return standings;
            }

            ConstructorStandingsDTO? cachedConstructorStandings = JsonConvert.DeserializeObject<ConstructorStandingsDTO>(result);

            if (cachedConstructorStandings == null)
            {
                throw new SerializationException("Could not deserialize cached result.");
            }

            return cachedConstructorStandings;
        }
    }
}