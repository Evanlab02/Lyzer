import { OverviewInterface } from "../../clients/interfaces/overviewInterfaces";

export const overviewMockIsNotRaceWeekend: OverviewInterface = {
    raceWeekendProgress: {
        name: "Practice 1 @ 12:00:00 UTC",
        ongoing: false,
        weekendProgress: 0
    }
}

export const overviewMockIsRaceWeekendOngoing: OverviewInterface = {
    raceWeekendProgress: {
        name: "Race @ 12:00:00 UTC",
        ongoing: true,
        weekendProgress: 100
    }
}
