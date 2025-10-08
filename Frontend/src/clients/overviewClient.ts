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
			const jsonContent = (await response.json()) as unknown;
			if (
				jsonContent &&
				typeof jsonContent === "object" &&
				"message" in jsonContent &&
				typeof jsonContent.message === "string"
			) {
				message = jsonContent.message;
			}
		} catch {
			console.warn("Failed to parse JSON, ignoring.");
		}
		throw new Error(`Failed to fetch overview data (${response.status.toString()} - ${message})`);
	}

	return (await response.json()) as OverviewInterface;
}
