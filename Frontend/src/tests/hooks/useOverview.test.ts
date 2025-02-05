import { expect, it } from "vitest";
import { renderHook, waitFor } from "@testing-library/react";
import useOverview from "../../hooks/useOverview";
import { overviewMockIsRaceWeekendOngoing } from "../mocks/overviewMock";
import { overviewMockIsNotRaceWeekend } from "../mocks/overviewMock";

it("should return the overview data that is not a race weekend", async () => {
	fetchMock.mockResponseOnce(JSON.stringify(overviewMockIsNotRaceWeekend));
	const { result } = renderHook(() => useOverview());

	await waitFor(() => {
		expect(result.current.raceWeekendProgress).toStrictEqual({
			name: "Practice 1 @ 12:00:00 UTC",
			ongoing: false,
			weekendProgress: 0
		});
	});
});

it("should return the overview data that is a race weekend and ongoing", async () => {
	fetchMock.mockResponseOnce(JSON.stringify(overviewMockIsRaceWeekendOngoing));
	const { result } = renderHook(() => useOverview());

	await waitFor(() => {
		expect(result.current.raceWeekendProgress).toStrictEqual({
			name: "Race @ 12:00:00 UTC",
			ongoing: true,
			weekendProgress: 100
		});
	});
});
