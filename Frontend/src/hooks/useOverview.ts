import { useEffect, useState } from "react";
import { OverviewInterface } from "../clients/interfaces/overviewInterfaces";
import { getOverview } from "../clients/overviewClient";

export default function useOverview() {
	const [overviewData, setOverviewData] = useState<OverviewInterface>();

	useEffect(() => {
		void fetchData();
	}, []);

	const fetchData = async () => {
		const result = await getOverview();
		setOverviewData(result);
	};

	return {
		raceWeekendProgress: overviewData?.raceWeekendProgress
	};
}