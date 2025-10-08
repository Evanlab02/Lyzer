import { OverviewInterface } from "./interfaces/overviewInterfaces";

export async function getOverview(): Promise<OverviewInterface> {
	const response = await fetch("/apis/lyzer/api/v1/lyzer/overview", {
		method: "GET",
		headers: {
			"Content-Type": "application/json",
		},
	});

	if (!response.ok || response.status !== 200) {
		let message = "No additional information";
		try {
			let jsonContent = await response.json();
			message = jsonContent["message"];
		} catch {
			console.warn("Failed to parse JSON, ignoring.");
		}
		throw new Error(`Failed to fetch overview data (${response.status} - ${message})`);
	}

	return (await response.json()) as OverviewInterface;
}
