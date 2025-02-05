import { OverviewInterface } from "./interfaces/overviewInterfaces";

export async function getOverview() : Promise<OverviewInterface> {
	const response = await fetch("https://catfact.ninja/fact", {
		method: "GET",
		headers: {
			"Content-Type": "application/json"
		}
	});

	if (!response.ok) {
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