using Lyzer.Clients;
using Lyzer.Common.Constants;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using Lyzer.Common.DTO;

namespace Lyzer.Services
{
    public class RacesService
    {
        private readonly ILogger<RacesService> _logger;
        private readonly JolpicaClient _client;
        private readonly CacheService _cache;
        public RacesService(ILogger<RacesService> logger, JolpicaClient client, CacheService cache) {
            _logger = logger;
            _client = client;
            _cache = cache;
        }
        public async Task<RacesDTO> GetCachedRaces(string season) 
        {
            string key = String.Format(CacheKeyConstants.Races, season);
            string? race = await _cache.Get(key);

            if (race == null)
            { 
                RacesDTO races = await _client.GetAllRacesForSeason(season);
                await _cache.Add(key, JsonConvert.SerializeObject(races), TimeSpan.FromHours(24));
                return races;
            }

            RacesDTO? cachedRaces = JsonConvert.DeserializeObject<RacesDTO>(race);

            if (cachedRaces == null)
            {
                throw new SerializationException("Could not deserialize cached races.");
            }

            return cachedRaces;
        }
    }
}
