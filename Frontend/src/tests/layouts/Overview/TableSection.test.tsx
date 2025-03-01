import { expect, it } from "vitest";
import { render } from "@testing-library/react";
import TableSection from "../../../layouts/Overview/TableSection";

it("TableSection renders correctly", async () => {
	const { findByTestId } = render(
		<div data-testid="lyzer-table-section">
			<TableSection />
		</div>
	);

	const tableSection = await findByTestId("lyzer-table-section");
	expect(tableSection).toMatchSnapshot();
});
