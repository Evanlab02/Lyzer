import { expect, it } from "vitest";
import { render } from "@testing-library/react";
import TableSection from "../../../layouts/Overview/TableSection";

it("TableSection renders loading state correctly", async () => {
	const { findByTestId } = render(
		<div data-testid="lyzer-table-section">
			<TableSection seasonProgress={undefined} />
		</div>
	);

	const tableSection = await findByTestId("lyzer-table-section");
	expect(tableSection).toMatchSnapshot();
});

it("TableSection renders correctly", async () => {
	const { findByTestId } = render(
		<div data-testid="lyzer-table-section">
			<TableSection 
				seasonProgress={{
					previousRaceWinner: "Max Verstappen",
					previousGrandPrix: "Qatar",
					seasonProgress: 12,
					seasonTotalRaces: 24
				}}
			/>
		</div>
	);

	const tableSection = await findByTestId("lyzer-table-section");
	expect(tableSection).toMatchSnapshot();
});
