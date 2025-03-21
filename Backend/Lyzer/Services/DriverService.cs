using System.Runtime.Serialization;

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

        public async Task<DriverStandingsDTO> GetCachedDriverStandings(string year)
        {
            string key = String.Format(CacheKeyConstants.DriverStandings, year);
            string? result = await _cache.Get(key);

            if (result == null)
            {
                DriverStandingsDTO standings = await _client.GetDriverStandings(year);
                await _cache.Add(key, JsonConvert.SerializeObject(standings), TimeSpan.FromHours(1));
                return standings;
            }

            DriverStandingsDTO? cachedDriverStandings = JsonConvert.DeserializeObject<DriverStandingsDTO>(result);

            if (cachedDriverStandings == null)
            {
                throw new SerializationException("Could not deserialize cached result.");
            }

            return cachedDriverStandings;
        }

        public async Task<List<OverviewDriverDTO>> GetCurrentDriverStandings()
        {
            var cachedDrivers = (await GetCachedDriverStandings("current")).DriverStandings;

            return cachedDrivers.Select(x => new OverviewDriverDTO
            {
                Name = $"{x.Driver.GivenName} {x.Driver.FamilyName}",
                Points = !string.IsNullOrEmpty(x.Points) ? Int32.Parse(x.Points) : 0,
                Position = !string.IsNullOrEmpty(x.Position) ? Int32.Parse(x.Position) : 0,
                Colour = ConstructorConstants.ConstructorColours.GetColourForConstructor(x.Constructors.First().ConstructorId)
            }).ToList();
        }
    }
}