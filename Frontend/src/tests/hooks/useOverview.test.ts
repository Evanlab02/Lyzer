import { expect, it } from "vitest";
import { renderHook, waitFor } from "@testing-library/react";
import useOverview from "../../hooks/useOverview";

it("should return the overview data", async () => {
	fetchMock.mockResponseOnce(JSON.stringify({}));
	const { result } = renderHook(() => useOverview());

	await waitFor(() => {
		expect(result.current.raceWeekendProgress).toStrictEqual({
			name: "Race @ 12:00:00 UTC",
			ongoing: true,
			weekendProgress: 100
		});
	});
});