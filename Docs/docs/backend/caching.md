# How we utilize Redis for caching

We use Redis as a caching solution for F1 data that is subject to change and based on the current year.

Currently, to keep things simple we use simple fixed time TTL (time to live) caching that might differ depending on the data we are trying to get.

## TTL cache for the different datasets

- Races/Schedules (24 hour TTL)
- Driver Standings (1 hour TTL)
- Constructor Standings (1 hour TTL)
- Race Results (1 hour TTL)

## How we retrive and store the cached data

Currently, the data that goes into the cache will be JSON strings. These are serialized on the API side when setting the cache and deserialized when retrieving it.

An example of a key value pair could be:

```bash
TODO: Pending
```
