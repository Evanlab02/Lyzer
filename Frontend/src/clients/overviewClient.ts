import { OVERVIEW_API_URL } from "../constants/urls";
import { buildFetchOptions, handleFetchResponse } from "./client";
import { OverviewData, OverviewDataSchema } from "./interfaces/overviewInterfaces";

export async function getOverview(): Promise<OverviewData> {
	const response = await fetch(OVERVIEW_API_URL, buildFetchOptions("GET"));
	return await handleFetchResponse(response, "Failed to fetch overview data", OverviewDataSchema);
}
