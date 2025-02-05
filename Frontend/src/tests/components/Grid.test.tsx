import { expect, it } from "vitest";
import { render } from "@testing-library/react";
import { GridContainer, GridItem } from "../../components/Grid";

it("Grid container renders correctly", () => {
	const { getByTestId } = render(
		<GridContainer testId="lyzer-grid-container">
			Grid Container
		</GridContainer>
	);

	const gridContainer = getByTestId("lyzer-grid-container");
	expect(gridContainer).toMatchSnapshot();
});

it("Grid container renders correctly with grid items", () => {
	const { getByTestId } = render(
		<GridContainer testId="lyzer-grid-container">
			<GridItem xs={12} sm={9} md={6} lg={4} xl={3} xxl={1}>Grid Item</GridItem>
		</GridContainer>
	);

	const gridContainer = getByTestId("lyzer-grid-container");
	expect(gridContainer).toMatchSnapshot();
});
