```
GET /api/v1/lyzer/overview
```

- **Authentication:** None required
- **Rate Limiting:** None
- **Cache TTL:** 15 minutes

The Overview endpoint provides comprehensive dashboard data for Formula 1, aggregating information about the current race weekend, upcoming events, season progress, and driver championship standings.

---

Returns an `OverviewDataDTO` object containing:

#### RaceWeekendProgress

Information about the current or upcoming race weekend progress.

| Field | Type | Description |
|-------|------|-------------|
| `name` | `string` | Name of the next session (e.g., "Practice 1", "Qualifying", "Race") |
| `ongoing` | `boolean` | Whether a session is currently in progress |
| `weekendProgress` | `integer` | Progress through the race weekend as a percentage (0-100) |
| `startDateTime` | `datetime` | Start date and time of the next session |

---

#### UpcomingRaceWeekend

Information about the proximity of the next race weekend.

| Field | Type | Description |
|-------|------|-------------|
| `isRaceWeekend` | `boolean` | Whether it is currently race weekend |
| `timeToRaceWeekendProgress` | `integer` | Progress toward the next race weekend as a percentage (0-100) |
| `status` | `string` | Human-readable status: "No", "Almost", or "It is race weekend!" |
| `timeToRaceWeekend` | `number` | Time remaining until race weekend starts, in minutes |

---

#### SeasonProgress

Information about the current F1 season's progress.

| Field | Type | Description |
|-------|------|-------------|
| `previousRaceWinner` | `string` | Full name of the driver who won the previous race |
| `previousGrandPrix` | `string` | Name of the previous Grand Prix |
| `seasonProgress` | `integer` | Number of races completed in the current season |
| `seasonTotalRaces` | `integer` | Total number of races scheduled for the season |

#### Drivers

Driver championship standings summary.

| Field | Type | Description |
|-------|------|-------------|
| `leader` | `string` | Full name of the championship leader |
| `color` | `string` | Hex color code of the leader's team (e.g., "#FF9800") |
| `standings` | `array` | List of driver standing entries (see below) |

**Driver Standing Entry:**

| Field | Type | Description |
|-------|------|-------------|
| `position` | `string` | Championship position |
| `driver` | `string` | Driver's full name |
| `points` | `string` | Total championship points |
| `color` | `string` | Hex color code of the driver's team |

### Example Response

```json
{
  "raceWeekendProgress": {
    "name": "Qualifying",
    "ongoing": false,
    "weekendProgress": 66,
    "startDateTime": "2024-11-23T18:00:00Z"
  },
  "upcomingRaceWeekend": {
    "isRaceWeekend": true,
    "timeToRaceWeekendProgress": 95,
    "status": "It is race weekend!",
    "timeToRaceWeekend": 120.5
  },
  "seasonProgress": {
    "previousRaceWinner": "Max Verstappen",
    "previousGrandPrix": "São Paulo Grand Prix",
    "seasonProgress": 21,
    "seasonTotalRaces": 24
  },
  "drivers": {
    "leader": "Max Verstappen",
    "color": "#1E5BC6",
    "standings": [
      {
        "position": "1",
        "driver": "Max Verstappen",
        "points": "393",
        "color": "#1E5BC6"
      },
      {
        "position": "2",
        "driver": "Lando Norris",
        "points": "331",
        "color": "#FF9800"
      }
    ]
  }
}
```

### Error Responses

#### 404 Not Found

Returned when no upcoming race is found in the schedule.

```json
{
  "message": "No upcoming race.",
  "details": ""
}
```

#### 500 Internal Server Error

Returned when no previous race can be found (should not occur in normal circumstances).

```json
{
  "message": "No previous race found.",
  "details": ""
}
```
