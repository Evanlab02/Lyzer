import { expect, it } from "vitest";
import { render } from "@testing-library/react";
import Overview from "../../pages/Overview";


it("Overview page renders correctly", async () => {
	fetchMock.mockResponseOnce(JSON.stringify({}));
	const { findByTestId } = render(
		<Overview />
	);

	const overviewPage = await findByTestId("lyzer-overview-page");
	expect(overviewPage).toMatchSnapshot();
});
