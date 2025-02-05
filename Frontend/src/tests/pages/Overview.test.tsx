import { expect, it } from "vitest";
import { render } from "@testing-library/react";
import Overview from "../../pages/Overview";


it("Overview page renders correctly", () => {
	const { getByTestId } = render(
		<Overview />
	);

	const overviewPage = getByTestId("lyzer-overview-page");
	expect(overviewPage).toMatchSnapshot();
});
