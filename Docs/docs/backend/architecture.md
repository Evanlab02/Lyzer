# General Structure of the API/Backend

The primary components to be aware of when it comes the backend are:

- Controllers
- Services
- Clients
- Repositories

There are also others like that play a less pivotal role but still form the project, such as:

- Constants
- DTOs
- Middleware
- Errors

## Controllers

The controllers are the entry point to the API from a consumer perspective, this is where endpoints are declared and exposed to the consumer.

They are primarily responsible for the routing and should be as un-sophisticated as possible, they call the relevant services to handle the business logic, data retrieval etc.

An example of a controller would be as below:

```csharp
using Lyzer.Common.DTO;
using Lyzer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Lyzer.Controllers
{
    [ApiController]
    [Route("/api/v1/drivers")]
    public class DriverController : ControllerBase
    {
        private readonly ILogger<DriverController> _logger;
        private readonly DriverService _driverService;

        public DriverController(ILogger<DriverController> logger, DriverService driverService)
        {
            _logger = logger;
            _driverService = driverService;
        }

        [HttpGet("standings", Name = "Get driver standings")]
        public async Task<ActionResult<DriverStandingsDTO>> GetCurrentDriverStandings()
        {
            return await _driverService.GetCurrentDriverStandings();
        }
    }
}
```

## Services

The services will most likely be the more complex and sophisticated pieces of the codebase as it would be here where we do heavy lifting.

The services are responsible for handling all business logic, transformation and calling the relevant data retrievel methods/functionality.

An example of a service would be as below:

```csharp
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

            if (result == null)
            {
                DriverStandingsDTO standings = await _client.GetCurrentDriverStandings();
                await _cache.Add(key, JsonConvert.SerializeObject(standings), TimeSpan.FromHours(1));
                return standings;
            }

            return JsonConvert.DeserializeObject<DriverStandingsDTO>(result) ?? new DriverStandingsDTO();
        }
    }
}
```

## Clients

Clients are responsible for communicating with other APIs. In the case of Lyzer, an example is a client to query Jolpica F1 data.

The clients should be doing minimal work and keeping everything as simple as possible, and focus primarily on communication with APIs.

They should *not* be performing transformations of anything of the sort.

An example implementation would be as below:

```csharp
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
            RestClientOptions options = new RestClientOptions(URIConstants.Jolpica.BaseUri);
            _client = new RestClient(options);
            _logger = logger;
        }

        public async Task<DriverStandingsDTO> GetCurrentDriverStandings()
        {
            string requestPath = String.Format(URIConstants.Jolpica.DriverStandingsUri, "current");
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
    }
}
```

## Repositories

Repositories are responsible for communicating with data storage such as a DB (database) like postgres, currently the project does not contain any repositories but in the future, we would very likely need it.

Please note that repositories are similiar to clients where buisness logic and transformation should be kept to a minimum and generally avoided in these classes.

## Constants

We contain classes with constants that will be values that are frequently used and if we make changes to them, should reflect accross the entire codebase.

This is easy to do with constants, as all values using the variable, would be updated.

An example would be:

```csharp
namespace Lyzer.Common.Constants
{
    public static class URIConstants
    {
        public static class Jolpica
        {
            public static string BaseUri { get; set; } = "https://api.jolpi.ca/ergast/f1";
            public static string DriverStandingsUri { get; set; } = "/{0}/driverstandings";
        }
    }
}
```

## DTOs

You might be familiar with schemas, models or the plethora of other things these could be called. In this codebase, they are reffered to as DTOs (Data transfer objects).

These objects are representations of what the API will return on successful calls.

An example being:

```csharp
namespace Lyzer.Common.DTO
{
    public class DriverDTO
    {
        public string DriverId { get; set; }
        public string PermanentNumber { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
    }
}
```

## Middleware

TODO

## Errors

TODO

## Diagram

TODO
