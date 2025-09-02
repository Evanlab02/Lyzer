using System.Runtime.Serialization;

using Lyzer.Clients;
using Lyzer.Common.Constants;
using Lyzer.Common.DTO;

using Newtonsoft.Json;

namespace Lyzer.Services
{
    public class ConstructorService(ILogger<ConstructorService> logger, CacheService cache, JolpicaClient client)
    {
        private readonly ILogger<ConstructorService> _logger = logger;
        private readonly CacheService _cache = cache;
        private readonly JolpicaClient _client = client;

        public async Task<ConstructorStandingsDTO> GetCachedConstructorStandings(string year)
        {
            string key = string.Format(CacheKeyConstants.ConstructorStandings, year);
            string? result = await _cache.Get(key);

            if (result == null)
            {
                ConstructorStandingsDTO standings = await _client.GetContructorsStandingsForYear(year);
                await _cache.Add(key, JsonConvert.SerializeObject(standings), TimeSpan.FromHours(1));
                return standings;
            }

            ConstructorStandingsDTO? cachedConstructorStandings = JsonConvert.DeserializeObject<ConstructorStandingsDTO>(result);
            return cachedConstructorStandings ?? throw new SerializationException("Could not deserialize cached result.");
        }
    }
}