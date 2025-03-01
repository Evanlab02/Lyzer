import { OverviewInterface } from "../../clients/interfaces/overviewInterfaces";

export const overviewMockIsNotRaceWeekend: OverviewInterface = {
	raceWeekendProgress: {
		name: "Practice 1",
		ongoing: false,
		weekendProgress: 0,
		startDateTime: "2025-02-22T12:00:00.000Z"
	}
};

export const overviewMockIsRaceWeekendOngoing: OverviewInterface = {
	raceWeekendProgress: {
		name: "Race",
		ongoing: true,
		weekendProgress: 100,
		startDateTime: "2025-02-22T12:00:00.000Z"
	}
};
