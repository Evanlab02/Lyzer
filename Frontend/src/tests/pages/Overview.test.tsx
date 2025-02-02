import { expect, it } from "vitest";
import { render } from "@testing-library/react";
import ThemeProvider from "../../components/ThemeProvider";
import Overview from "../../pages/Overview";


it("Overview page renders correctly", () => {
	const { getByTestId } = render(
		<ThemeProvider>
			<Overview />
		</ThemeProvider>
	);

	const overviewPage = getByTestId("lyzer-overview-page");
	expect(overviewPage).toMatchSnapshot();
});
