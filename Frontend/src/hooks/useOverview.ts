import { useEffect, useState } from "react";
import { RaceWeekendProgress, UpcomingRaceWeekend } from "../clients/interfaces/overviewInterfaces";
import { getOverview } from "../clients/overviewClient";

export default function useOverview() {
	const [raceWeekendProgress, setRaceWeekendProgress] = useState<RaceWeekendProgress>();
	const [upcomingRaceWeekend, setUpcomingRaceWeekend] = useState<UpcomingRaceWeekend>();

	useEffect(() => {
		void fetchData();
	}, []);

	const fetchData = async () => {
		const result = await getOverview();
		setRaceWeekendProgress(result.raceWeekendProgress);
		setUpcomingRaceWeekend(result.upcomingRaceWeekend);
	};

	const refreshData = () => {
		setRaceWeekendProgress(undefined);
		setUpcomingRaceWeekend(undefined);
		void fetchData();
	};

	return {
		raceWeekendProgress,
		upcomingRaceWeekend,
		refreshData
	};
}