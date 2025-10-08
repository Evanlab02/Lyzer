using System.Runtime.Serialization;

using Lyzer.Clients;
using Lyzer.Common.Constants;
using Lyzer.Common.DTO;
using Lyzer.Common.Helpers;

using Newtonsoft.Json;

namespace Lyzer.Services
{
    public class RacesService(ILogger<RacesService> logger, JolpicaClient client, CacheService cache)
    {
        private readonly ILogger<RacesService> _logger = logger;
        private readonly JolpicaClient _client = client;
        private readonly CacheService _cache = cache;

        public async Task<RacesDTO> GetCachedRaces(string season)
        {
            string key = string.Format(CacheKeyConstants.Races, season);
            string? race = await _cache.Get(key);

            if (race == null)
            {
                RacesDTO races = await _client.GetAllRacesForSeason(season);
                await _cache.Add(key, JsonConvert.SerializeObject(races), TimeSpan.FromHours(24));
                return races;
            }

            RacesDTO? cachedRaces = JsonConvert.DeserializeObject<RacesDTO>(race);
            return cachedRaces ?? throw new SerializationException("Could not deserialize cached races.");
        }
    }
}