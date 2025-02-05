import { OverviewInterface } from "./interfaces/overviewInterfaces";

export async function getOverview() : Promise<OverviewInterface> {
	await fetch("https://catfact.ninja/fact", {
		method: "GET",
		headers: {
			"Content-Type": "application/json"
		}
	});

	return {
		raceWeekendProgress: {
			name: "Qualifying @ 12:00:00 UTC",
			ongoing: true,
			weekendProgress: 80
		}
	} as OverviewInterface;
}