export interface OverviewInterface {
    raceWeekendProgress: RaceWeekendProgress;
    upcomingRaceWeekend: UpcomingRaceWeekend;
    seasonProgress: SeasonProgress;
}

export interface RaceWeekendProgress {
    name: string;
    ongoing: boolean;
    weekendProgress: number;
    startDateTime?: string;
}

export interface UpcomingRaceWeekend {
    isRaceWeekend: boolean;
    timeToRaceWeekendProgress: number;
    timeToRaceWeekend: number;
    status: string;
}


export interface SeasonProgress {
    previousRaceWinner: string;
    previousGrandPrix: string;
    seasonProgress: number;
    seasonTotalRaces: number;
}