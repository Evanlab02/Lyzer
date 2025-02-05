import { OverviewInterface } from "./interfaces/overviewInterfaces";

export async function getOverview() : Promise<OverviewInterface> {
	const response = await fetch("/apis/lyzer/api/v1/lyzer/overview", {
		method: "GET",
		headers: {
			"Content-Type": "application/json"
		}
	});

	if (!response.ok || response.status !== 200) {
		throw new Error("Failed to fetch overview data.");
	}

	return await response.json() as OverviewInterface;

	// return {
	// 	raceWeekendProgress: {
	// 		name: "Race @ 12:00:00 UTC",
	// 		ongoing: true,
	// 		weekendProgress: 100
	// 	}
	// } as OverviewInterface;
}