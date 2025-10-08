import { useCallback, useEffect, useState } from "react";
import { OverviewInterface } from "../clients/interfaces/overviewInterfaces";
import { getOverview } from "../clients/overviewClient";

const EMPTY: OverviewInterface = {
	raceWeekendProgress: {
		name: "",
		ongoing: false,
		weekendProgress: 0,
		startDateTime: "",
	},
	upcomingRaceWeekend: {
		isRaceWeekend: false,
		timeToRaceWeekendProgress: 0,
		status: "",
		timeToRaceWeekend: 0,
	},
	seasonProgress: {
		previousRaceWinner: "",
		previousGrandPrix: "",
		seasonProgress: 0,
		seasonTotalRaces: 0,
	},
	drivers: {
		leader: "",
		color: "",
		standings: [],
	},
};

export default function useOverview() {
	const [data, setData] = useState<OverviewInterface>(EMPTY);
	const [isLoading, setIsLoading] = useState(true);
	const [error, setError] = useState<Error>();

	const fetchData = useCallback(async () => {
		try {
			setIsLoading(true);
			setError(undefined);
			const result = await getOverview();
			setData(result);
		} catch (err) {
			setError(err instanceof Error ? err : new Error("Failed to fetch overview data"));
		} finally {
			setIsLoading(false);
		}
	}, []);

	useEffect(() => {
		void fetchData();
	}, [fetchData]);

	const refreshData = useCallback(() => {
		void fetchData();
	}, [fetchData]);

	return {
		...data,
		isLoading,
		error,
		refreshData,
	};
}
