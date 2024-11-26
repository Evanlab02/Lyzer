using Lyzer.Clients;
using Lyzer.Common.Constants;
using Lyzer.Common.DTO;
using Newtonsoft.Json;

namespace Lyzer.Services
{
    public class DriverService
    {
        private readonly ILogger<DriverService> _logger;
        private readonly JolpicaClient _client;
        private readonly CacheService _cache;

        public DriverService(ILogger<DriverService> logger, JolpicaClient client, CacheService cache)
        {
            _logger = logger;
            _client = client;
            _cache = cache;
        }

        public async Task<DriverStandingsDTO> GetCurrentDriverStandings()
        {
            string key = String.Format(CacheKeyConstants.DriverStandings, "current");
            string? result = await _cache.Get(key);

            if (result == null) {
                DriverStandingsDTO standings = await _client.GetCurrentDriverStandings();
                await _cache.Add(key, JsonConvert.SerializeObject(standings), TimeSpan.FromHours(1));
                return standings;
            }

            return JsonConvert.DeserializeObject<DriverStandingsDTO>(result) ?? new DriverStandingsDTO();
        }
    }
}
