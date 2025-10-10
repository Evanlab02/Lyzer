import { render } from "@testing-library/react";
import { expect, it } from "vitest";
import CardSection from "../../../layouts/Overview/CardSection";

it("CardSection renders loading state correctly", async () => {
	const { findByTestId } = render(
		<div data-testid="lyzer-card-section">
			<CardSection
				loading
				leader=""
				leaderColor=""
				raceWeekendProgress={{
					name: "",
					ongoing: false,
					weekendProgress: 0,
					startDateTime: undefined,
				}}
				upcomingRaceWeekend={{
					isRaceWeekend: false,
					status: "",
					timeToRaceWeekend: 0,
					timeToRaceWeekendProgress: 0,
				}}
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
				loading={false}
				leader="Oscar Piastri"
				leaderColor="#FF9800"
				raceWeekendProgress={{
					name: "Race",
					ongoing: true,
					weekendProgress: 100,
					startDateTime: "2025-02-22T12:00:00.000Z",
				}}
				upcomingRaceWeekend={{
					isRaceWeekend: true,
					status: "Yes",
					timeToRaceWeekendProgress: 100,
					timeToRaceWeekend: 0,
				}}
			/>
		</div>
	);

	const cardSection = await findByTestId("lyzer-card-section");
	expect(cardSection).toMatchSnapshot();
});
