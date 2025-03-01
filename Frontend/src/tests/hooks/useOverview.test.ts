import { expect, it } from "vitest";
import { renderHook, waitFor } from "@testing-library/react";
import useOverview from "../../hooks/useOverview";
import { overviewMockIsRaceWeekendOngoing, overviewMockIsNotRaceWeekend } from "../mocks/overviewMock";

it("should return the overview data that is not a race weekend", async () => {
	fetchMock.mockResponseOnce(JSON.stringify(overviewMockIsNotRaceWeekend));
	const { result } = renderHook(() => useOverview());

	await waitFor(() => {
		expect(result.current.raceWeekendProgress).toStrictEqual({
			name: "Practice 1",
			ongoing: false,
			weekendProgress: 0,
			startDateTime: "2025-02-22T12:00:00.000Z"
		});
	});

	await waitFor(() => {
		expect(result.current.upcomingRaceWeekend).toStrictEqual({
			isRaceWeekend: false,
			status: "No",
			timeToRaceWeekendProgress: 50,
			timeToRaceWeekend: 4000
		});
	});
});

it("should return the overview data that is a race weekend and ongoing", async () => {
	fetchMock.mockResponseOnce(JSON.stringify(overviewMockIsRaceWeekendOngoing));
	const { result } = renderHook(() => useOverview());

	await waitFor(() => {
		expect(result.current.raceWeekendProgress).toStrictEqual({
			name: "Race",
			ongoing: true,
			weekendProgress: 100,
			startDateTime: "2025-02-22T12:00:00.000Z"
		});
	});

	await waitFor(() => {
		expect(result.current.upcomingRaceWeekend).toStrictEqual({
			isRaceWeekend: true,
			status: "Yes",
			timeToRaceWeekendProgress: 100,
			timeToRaceWeekend: 0
		});
	});
});

it("should return new data when refreshData is called", async () => {
	fetchMock.mockResponseOnce(JSON.stringify(overviewMockIsNotRaceWeekend));
	fetchMock.mockResponseOnce(JSON.stringify(overviewMockIsRaceWeekendOngoing));

	const { result } = renderHook(() => useOverview());

	await waitFor(() => {
		expect(result.current.raceWeekendProgress).toStrictEqual(overviewMockIsNotRaceWeekend.raceWeekendProgress);
		expect(result.current.upcomingRaceWeekend).toStrictEqual(overviewMockIsNotRaceWeekend.upcomingRaceWeekend);
	});

	await waitFor(() => {
		result.current.refreshData();
	});

	await waitFor(() => {
		expect(result.current.raceWeekendProgress).toStrictEqual(overviewMockIsRaceWeekendOngoing.raceWeekendProgress);
		expect(result.current.upcomingRaceWeekend).toStrictEqual(overviewMockIsRaceWeekendOngoing.upcomingRaceWeekend);
	});
});
