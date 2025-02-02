import { expect, it } from "vitest";
import { MemoryRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import ThemeProvider from "../../components/ThemeProvider";
import Navigation from "../../routes/Navigation";


it("Navigation renders overview page correctly", async () => {
	const { findByTestId } = render(
		<MemoryRouter initialEntries={["/"]}>
			<ThemeProvider>
				<Navigation />
			</ThemeProvider>
		</MemoryRouter>
	);

	const overviewPage = await findByTestId("lyzer-overview-page");
	expect(overviewPage).toMatchSnapshot();
});

it("Navigation renders 404 page correctly", async () => {
	const { findByTestId } = render(
		<MemoryRouter initialEntries={["/non-existent-route"]}>
			<ThemeProvider>
				<Navigation />
			</ThemeProvider>
		</MemoryRouter>
	);

	const notFoundPage = await findByTestId("lyzer-not-found-page");
	expect(notFoundPage).toMatchSnapshot();
});
