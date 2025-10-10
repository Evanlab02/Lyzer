import { renderHook, waitFor } from "@testing-library/react";
import { expect, it } from "vitest";
import useOverview from "../../hooks/useOverview";
import {
	overviewMockIsNotRaceWeekend,
	overviewMockIsRaceWeekendOngoing,
} from "../mocks/overviewMock";

it("should return the overview data that is not a race weekend", async () => {
	fetchMock.mockResponseOnce(JSON.stringify(overviewMockIsNotRaceWeekend));
	const { result } = renderHook(() => useOverview());

	await waitFor(() => {
		expect(result.current.raceWeekendProgress).toStrictEqual({
			name: "First Practice",
			ongoing: false,
			weekendProgress: 0,
			startDateTime: "2025-10-17T17:30:00Z",
		});
	});

	await waitFor(() => {
		expect(result.current.upcomingRaceWeekend).toStrictEqual({
			isRaceWeekend: false,
			status: "No",
			timeToRaceWeekendProgress: 0,
			timeToRaceWeekend: 10255,
		});
	});

	await waitFor(() => {
		expect(result.current.seasonProgress).toStrictEqual({
			previousRaceWinner: "George Russell",
			previousGrandPrix: "Singapore Grand Prix",
			seasonProgress: 18,
			seasonTotalRaces: 24,
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
			startDateTime: "2025-02-22T12:00:00.000Z",
		});
	});

	await waitFor(() => {
		expect(result.current.upcomingRaceWeekend).toStrictEqual({
			isRaceWeekend: true,
			status: "Yes",
			timeToRaceWeekendProgress: 100,
			timeToRaceWeekend: 0,
		});
	});

	await waitFor(() => {
		expect(result.current.seasonProgress).toStrictEqual({
			previousRaceWinner: "George Russell",
			previousGrandPrix: "Singapore Grand Prix",
			seasonProgress: 18,
			seasonTotalRaces: 24,
		});
	});
});

it("should return new data when refreshData is called", async () => {
	fetchMock.mockResponseOnce(JSON.stringify(overviewMockIsNotRaceWeekend));
	fetchMock.mockResponseOnce(JSON.stringify(overviewMockIsRaceWeekendOngoing));

	const { result } = renderHook(() => useOverview());

	await waitFor(() => {
		expect(result.current.raceWeekendProgress).toStrictEqual(
			overviewMockIsNotRaceWeekend.raceWeekendProgress
		);
		expect(result.current.upcomingRaceWeekend).toStrictEqual(
			overviewMockIsNotRaceWeekend.upcomingRaceWeekend
		);
		expect(result.current.seasonProgress).toStrictEqual(
			overviewMockIsNotRaceWeekend.seasonProgress
		);
	});

	await waitFor(() => {
		result.current.fetchData();
	});

	await waitFor(() => {
		expect(result.current.raceWeekendProgress).toStrictEqual(
			overviewMockIsRaceWeekendOngoing.raceWeekendProgress
		);
		expect(result.current.upcomingRaceWeekend).toStrictEqual(
			overviewMockIsRaceWeekendOngoing.upcomingRaceWeekend
		);
		expect(result.current.seasonProgress).toStrictEqual(
			overviewMockIsRaceWeekendOngoing.seasonProgress
		);
	});
});
