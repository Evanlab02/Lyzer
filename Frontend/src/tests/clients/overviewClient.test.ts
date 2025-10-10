import { expect, it } from "vitest";
import { getOverview } from "../../clients/overviewClient";
import {
	overviewMockIsNotRaceWeekend,
	overviewMockIsRaceWeekendOngoing,
} from "../mocks/overviewMock";

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

	await expect(callWrapper).rejects.toThrow(
		"Failed to fetch overview data (404 - No additional information)"
	);
});

it("getOverview fails with API message", async () => {
	fetchMock.mockResponseOnce(JSON.stringify({ message: "Server is down" }), { status: 500 });

	const callWrapper = async () => {
		await getOverview();
	};

	await expect(callWrapper).rejects.toThrow("Failed to fetch overview data (500 - Server is down)");
});

it("getOverview fails when the response is not the correct format", async () => {
	fetchMock.mockResponseOnce(JSON.stringify({}));

	const callWrapper = async () => {
		await getOverview();
	};

	await expect(callWrapper).rejects.toThrow();
});

it("getOverview shows default error information if response is not JSON but is invalid status code. ", async () => {
	fetchMock.mockResponseOnce("", { status: 500 });

	const callWrapper = async () => {
		await getOverview();
	};

	await expect(callWrapper).rejects.toThrow(
		"Failed to fetch overview data (500 - No additional information)"
	);
});

it("getOverview fails if not JSON but did get 200 status code", async () => {
	fetchMock.mockResponseOnce("");

	const callWrapper = async () => {
		await getOverview();
	};

	await expect(callWrapper).rejects.toThrow();
});
