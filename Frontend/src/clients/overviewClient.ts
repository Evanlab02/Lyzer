import { OverviewInterface } from "./interfaces/overviewInterfaces";

export async function getOverview() : Promise<OverviewInterface> {
	const response: Response = await fetch("https://catfact.ninja/fact", {
		method: "GET",
		headers: {
			"Content-Type": "application/json"
		}
	});
	return (await response.json()) as OverviewInterface;
}