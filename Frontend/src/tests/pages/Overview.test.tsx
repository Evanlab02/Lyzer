import { expect, it } from "vitest";
import { render } from "@testing-library/react";
import Overview from "../../pages/Overview";
import { OverviewInterface } from "../../clients/interfaces/overviewInterfaces";

it("Given the user is on the overview page when it is race weekend and there is no ongoing session, then the user should be able to see what the next session of the race weekend will be.", async () => {
	const mockData = {
		raceWeekendProgress: {
			name: "Qualifying 3",
			ongoing: false,
			weekendProgress: 80,
			startDateTime: "2025-02-22T12:00:00.000Z"
		},
		upcomingRaceWeekend: {
			isRaceWeekend: true,
			status: "Yes",
			timeToRaceWeekendProgress: 100,
			timeToRaceWeekend: 0
		}
	} as OverviewInterface;

	fetchMock.mockResponseOnce(JSON.stringify(mockData));
	const { findByTestId } = render(
		<Overview />
	);

	const overviewPage = await findByTestId("lyzer-overview-page");
	expect(overviewPage).toMatchSnapshot();
});

it("Given the user is on the overview page when it is race weekend and there is an ongoing session, then the user should be able to see what the current ongoing session is.", async () => {
	const mockData = {
		raceWeekendProgress: {
			name: "Race",
			ongoing: true,
			weekendProgress: 100,
			startDateTime: "2025-02-22T12:00:00.000Z"
		},
		upcomingRaceWeekend: {
			isRaceWeekend: true,
			status: "Yes",
			timeToRaceWeekendProgress: 100,
			timeToRaceWeekend: 0
		}
	} as OverviewInterface;

	fetchMock.mockResponseOnce(JSON.stringify(mockData));
	const { findByTestId } = render(
		<Overview />
	);

	const overviewPage = await findByTestId("lyzer-overview-page");
	expect(overviewPage).toMatchSnapshot();
});

it("Given the user is on the overview page when it is race weekend and we are halfway through the weekend, then the progress bar should indicate we are 50% through the weekend.", async () => {
	const mockData = {
		raceWeekendProgress: {
			name: "Qualifying 3",
			ongoing: false,
			weekendProgress: 50,
			startDateTime: "2025-02-22T12:00:00.000Z"
		},
		upcomingRaceWeekend: {
			isRaceWeekend: true,
			status: "Yes",
			timeToRaceWeekendProgress: 100,
			timeToRaceWeekend: 0
		}
	} as OverviewInterface;

	fetchMock.mockResponseOnce(JSON.stringify(mockData));
	const { findByTestId } = render(
		<Overview />
	);
	
	const overviewPage = await findByTestId("lyzer-overview-page");
	expect(overviewPage).toMatchSnapshot();
});

it("Given the user is on the overview page when it is not a race weekend, then the progress bar should be empty and the next session should be the first session of the next weekend (Most likely practice 1).", async () => {
	const mockData = {
		raceWeekendProgress: {
			name: "Practice 1",
			ongoing: false,
			weekendProgress: 0,
			startDateTime: "2025-02-22T12:00:00.000Z"
		},
		upcomingRaceWeekend: {
			isRaceWeekend: false,
			status: "Almost",
			timeToRaceWeekendProgress: 90,
			timeToRaceWeekend: 600
		}
	} as OverviewInterface;

	fetchMock.mockResponseOnce(JSON.stringify(mockData));
	const { findByTestId } = render(
		<Overview />
	);

	const overviewPage = await findByTestId("lyzer-overview-page");
	expect(overviewPage).toMatchSnapshot();
});

it("Given: User is on the overview page When: It is not race weekend Then: User should be able to see that it is not race weekend.", async () => {
	const mockData = {
		raceWeekendProgress: {
			name: "Practice 1",
			ongoing: false,
			weekendProgress: 0,
			startDateTime: "2025-02-22T12:00:00.000Z"
		},
		upcomingRaceWeekend: {
			isRaceWeekend: false,
			status: "No",
			timeToRaceWeekendProgress: 50,
			timeToRaceWeekend: 7200
		}
	} as OverviewInterface;

	fetchMock.mockResponseOnce(JSON.stringify(mockData));
	const { findByTestId } = render(
		<Overview />
	);

	const overviewPage = await findByTestId("lyzer-overview-page");
	expect(overviewPage).toMatchSnapshot();
});

it("Given: User is on the overview page When: It is less than 24 hours to go until race weekend Then: User should be able to see that it is almost race weekend.", async () => {
	const mockData = {
		raceWeekendProgress: {
			name: "Practice 1",
			ongoing: false,
			weekendProgress: 0,
			startDateTime: "2025-02-22T12:00:00.000Z"
		},
		upcomingRaceWeekend: {
			isRaceWeekend: false,
			status: "Almost",
			timeToRaceWeekendProgress: 80,
			timeToRaceWeekend: 600
		}
	} as OverviewInterface;

	fetchMock.mockResponseOnce(JSON.stringify(mockData));
	const { findByTestId } = render(
		<Overview />
	);

	const overviewPage = await findByTestId("lyzer-overview-page");
	expect(overviewPage).toMatchSnapshot();
});

it("Given: User is on the overview page When: It is race weekend Then: User should be able to see that it is race weekend.", async () => {
	const mockData = {
		raceWeekendProgress: {
			name: "Race",
			ongoing: true,
			weekendProgress: 100,
			startDateTime: "2025-02-22T12:00:00.000Z"
		},
		upcomingRaceWeekend: {
			isRaceWeekend: true,
			status: "Yes",
			timeToRaceWeekendProgress: 100,
			timeToRaceWeekend: 0
		}
	} as OverviewInterface;

	fetchMock.mockResponseOnce(JSON.stringify(mockData));
	const { findByTestId } = render(
		<Overview />
	);

	const overviewPage = await findByTestId("lyzer-overview-page");
	expect(overviewPage).toMatchSnapshot();
});
