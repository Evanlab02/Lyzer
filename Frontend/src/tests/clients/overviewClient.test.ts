import { expect, it } from "vitest";
import { getOverview } from "../../clients/overviewClient";
import { overviewMockIsNotRaceWeekend, overviewMockIsRaceWeekendOngoing } from "../mocks/overviewMock";


it("getOverview returns overview data that is not a race weekend", async () => {
	fetchMock.mockResponseOnce(JSON.stringify(overviewMockIsNotRaceWeekend));
	const overview = await getOverview();
	expect(overview).toStrictEqual(overviewMockIsNotRaceWeekend);
});

it("getOverview returns overview data that is a race weekend and ongoing", async () => {
	fetchMock.mockResponseOnce(JSON.stringify(overviewMockIsRaceWeekendOngoing));
	const overview = await getOverview();
	expect(overview).toStrictEqual(overviewMockIsRaceWeekendOngoing);
});

it("getOverview fails when the response is not ok", async () => {
	fetchMock.mockResponseOnce(JSON.stringify(overviewMockIsNotRaceWeekend), { status: 404 });

	const callWrapper = async () => {
		await getOverview();
	};

	await expect(callWrapper).rejects.toThrow("Failed to fetch overview data.");
});

