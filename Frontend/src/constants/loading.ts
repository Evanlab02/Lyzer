import { RaceWeekendProgress, SeasonProgress, UpcomingRaceWeekend } from "../clients/interfaces/overviewInterfaces";

export const LOADING_RACE_WEEKEND_PROGRESS = {
	name: "Loading...",
	ongoing: false,
	weekendProgress: 0,
	startDateTime: undefined
} as RaceWeekendProgress;

export const LOADING_UPCOMING_RACE_WEEKEND = {
	isRaceWeekend: false,
	status: "No",
	timeToRaceWeekendProgress: 0,
	timeToRaceWeekend: 0
} as UpcomingRaceWeekend;


export const LOADING_SEASON_PROGRESS = {
	previousRaceWinner: "Loading...",
	previousGrandPrix: "Loading...",
	seasonProgress: 0,
	seasonTotalRaces: 0
} as SeasonProgress;
