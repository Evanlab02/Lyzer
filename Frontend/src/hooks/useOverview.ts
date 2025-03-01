import { useEffect, useState } from "react";
import { RaceWeekendProgress } from "../clients/interfaces/overviewInterfaces";
import { getOverview } from "../clients/overviewClient";

export default function useOverview() {
	const [raceWeekendProgress, setRaceWeekendProgress] = useState<RaceWeekendProgress>();

	useEffect(() => {
		void fetchData();
	}, []);

	const fetchData = async () => {
		const result = await getOverview();
		setRaceWeekendProgress(result.raceWeekendProgress);
	};

	const refreshData = () => {
		setRaceWeekendProgress(undefined);
		void fetchData();
	};

	return {
		raceWeekendProgress,
		refreshData
	};
}