import { expect, it } from "vitest";
import { render } from "@testing-library/react";
import CardSection from "../../../layouts/Overview/CardSection";


it("CardSection renders loading state correctly", async () => {
	const { findByTestId } = render(
		<div data-testid="lyzer-card-section">
			<CardSection 
				raceWeekendProgress={undefined}
				upcomingRaceWeekend={undefined}
			/>
		</div>
	);

	const cardSection = await findByTestId("lyzer-card-section");
	expect(cardSection).toMatchSnapshot();
});

it("CardSection renders correctly", async () => {
	const { findByTestId } = render(
		<div data-testid="lyzer-card-section">
			<CardSection 
				raceWeekendProgress={{
					name: "Race",
					ongoing: true,
					weekendProgress: 100,
					startDateTime: "2025-02-22T12:00:00.000Z"
				}}
				upcomingRaceWeekend={{
					isRaceWeekend: true,
					status: "Yes",
					timeToRaceWeekendProgress: 100,
					timeToRaceWeekend: 0
				}}
			/>
		</div>
	);

	const cardSection = await findByTestId("lyzer-card-section");
	expect(cardSection).toMatchSnapshot();
});
