export interface OverviewInterface {
    raceWeekendProgress: RaceWeekendProgress;
    upcomingRaceWeekend: UpcomingRaceWeekend;
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
