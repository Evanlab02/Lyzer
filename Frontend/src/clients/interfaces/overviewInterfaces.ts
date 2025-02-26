export interface OverviewInterface {
    raceWeekendProgress: RaceWeekendProgress;
}

export interface RaceWeekendProgress {
    name: string;
    ongoing: boolean;
    weekendProgress: number;
    startDateTime?: string;
}
