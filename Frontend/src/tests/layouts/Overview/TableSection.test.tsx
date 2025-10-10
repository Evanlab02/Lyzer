import { render } from "@testing-library/react";
import { expect, it } from "vitest";
import TableSection from "../../../layouts/Overview/TableSection";
import { overviewMockIsNotRaceWeekend } from "../../mocks/overviewMock";

it("TableSection renders loading state correctly", async () => {
	const { findByTestId } = render(
		<div data-testid="lyzer-table-section">
			<TableSection
				loading
				seasonProgress={{
					previousRaceWinner: "",
					previousGrandPrix: "",
					seasonProgress: 0,
					seasonTotalRaces: 0,
				}}
				driverStandings={[]}
			/>
		</div>
	);

	const tableSection = await findByTestId("lyzer-table-section");
	expect(tableSection).toMatchSnapshot();
});

it("TableSection renders correctly", async () => {
	const { findByTestId } = render(
		<div data-testid="lyzer-table-section">
			<TableSection
				loading={false}
				seasonProgress={{
					previousRaceWinner: "Max Verstappen",
					previousGrandPrix: "Qatar",
					seasonProgress: 12,
					seasonTotalRaces: 24,
				}}
				driverStandings={overviewMockIsNotRaceWeekend.drivers.standings}
			/>
		</div>
	);

	const tableSection = await findByTestId("lyzer-table-section");
	expect(tableSection).toMatchSnapshot();
});
