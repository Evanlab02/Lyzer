import { OverviewInterface } from "../../clients/interfaces/overviewInterfaces";

export const overviewMockIsNotRaceWeekend: OverviewInterface = {
	raceWeekendProgress: {
		name: "Practice 1",
		ongoing: false,
		weekendProgress: 0,
		startDateTime: "2025-02-22T12:00:00.000Z"
	},
	upcomingRaceWeekend: {
		isRaceWeekend: false,
		status: "No",
		timeToRaceWeekendProgress: 50,
		timeToRaceWeekend: 4000
	}
};

export const overviewMockIsRaceWeekendOngoing: OverviewInterface = {
	raceWeekendProgress: {
		name: "Race",
		ongoing: true,
		weekendProgress: 100,
		startDateTime: "2025-02-22T12:00:00.000Z"
	}, 
	upcomingRaceWeekend: {
		isRaceWeekend: true,
		status: "Yes",
		timeToRaceWeekendProgress: 100,
		timeToRaceWeekend: 0
	}
};
