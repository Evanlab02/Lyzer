import { useEffect, useState } from "react";
import { RaceWeekendProgress, SeasonProgress, UpcomingRaceWeekend } from "../clients/interfaces/overviewInterfaces";
import { getOverview } from "../clients/overviewClient";

export default function useOverview() {
	const [raceWeekendProgress, setRaceWeekendProgress] = useState<RaceWeekendProgress>();
	const [upcomingRaceWeekend, setUpcomingRaceWeekend] = useState<UpcomingRaceWeekend>();
	const [seasonProgress, setSeasonProgress] = useState<SeasonProgress>();

	useEffect(() => {
		void fetchData();
	}, []);

	const fetchData = async () => {
		const result = await getOverview();
		setRaceWeekendProgress(result.raceWeekendProgress);
		setUpcomingRaceWeekend(result.upcomingRaceWeekend);
		setSeasonProgress(result.seasonProgress);
	};

	const refreshData = () => {
		setRaceWeekendProgress(undefined);
		setUpcomingRaceWeekend(undefined);
		setSeasonProgress(undefined);
		void fetchData();
	};

	return {
		raceWeekendProgress,
		upcomingRaceWeekend,
		seasonProgress,
		refreshData
	};
}