import { IndianRupee } from "lucide-react";

export interface OverviewInterface {
    raceWeekendProgress: RaceWeekendProgress;
}

export interface RaceWeekendProgress {
    name: string;
    ongoing: boolean;
    weekendProgress: number;
}

export interface UpcomingRaceWeekend {
    isRaceWeekend: boolean;
    timeToRaceWeekendProgress: number; // value between 0 and 100
    status: string; // e.g., "It is race weekend!", "Almost", "No"
    timeToRaceWeekend: number; // in minutes
  }


