import { expect, it } from "vitest";
import CardSection from "../../layouts/Overview/CardSection";
import { render } from "@testing-library/react";

it("CardSection renders loading state correctly", async () => {
	const { findByTestId } = render(
		<div data-testid="lyzer-card-section">
            <CardSection 
                raceWeekendProgress={undefined}
            />
        </div>
	);

	const cardSection = await findByTestId("lyzer-card-section");
	expect(cardSection).toMatchSnapshot();
});
