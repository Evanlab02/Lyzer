# How We Utilize Redis for Caching

We use Redis as a caching solution for F1 data that is subject to change and based on the current year.

To keep things simple, we use fixed time-to-live (TTL) caching, which may vary depending on the data being cached.

## TTL Cache for Different Datasets

- **Races/Schedules**: 24-hour TTL
- **Driver Standings**: 1-hour TTL
- **Constructor Standings**: 1-hour TTL
- **Race Results**: 1-hour TTL

## How We Retrieve and Store Cached Data

Currently, the data stored in the cache are JSON strings. These strings are serialized on the API side when setting the cache and deserialized when retrieving it.

Below you can find an example on how we serialize/deserialize those values with C# using the CacheService we have implemented in the codebase:

```C#
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
```
