# High level architecture overview

This project is under active development, so the architecture is very much subject to change.

## Components

- Jolpica F1 API
- Redis
- Lyzer API
    - C#
    - ASP.NET


### Jolpica F1 API

The jolpica F1 API is a successor to the Ergast F1 API, that can be found [here](https://api.jolpi.ca/ergast/f1).

This is currently the primary data source for all F1 data provided by Lyzer.

### Redis

We use redis to cache data for the current year, instead of storing it in a more pernament location, as this data is subject to change.

Some examples being:

- Driver Standings
- Constructor Standings
- Races for the season

### Lyzer API

A C#, ASP.NET powered backend. Currently its main roles are the following:

- Provide formula 1 data on the current year.
    - If data is not cached, retrieve it from the Jolpica API and cache it.
    - Otherwise, use the cached value.

## Diagram

TODO: Pending
