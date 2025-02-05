import { expect, it } from "vitest";
import { render } from "@testing-library/react";
import Progress from "../../components/Progress";

it("Empty progress bar renders correctly", () => {
	const { getByTestId } = render(
		<Progress 
			testId="lyzer-progress-bar"
			value={0}
			max={100}
		/>
	);
	const progressBar = getByTestId("lyzer-progress-bar");
	expect(progressBar).toMatchSnapshot();
});

it("Progress bar renders correctly with value", () => {
	const { getByTestId } = render(
		<Progress 
			testId="lyzer-progress-bar"
			value={50}
			max={100}
		/>
	);
	const progressBar = getByTestId("lyzer-progress-bar");
	expect(progressBar).toMatchSnapshot();
});

it("Progress bar renders correctly with 50% value", () => {
	const { getByTestId } = render(
		<Progress 
			testId="lyzer-progress-bar"
			value={50}
			max={100}
			useProgressColour
		/>
	);
	const progressBar = getByTestId("lyzer-progress-bar");
	expect(progressBar).toMatchSnapshot();
});


it("Progress bar renders correctly with 80% value", () => {
	const { getByTestId } = render(
		<Progress 
			testId="lyzer-progress-bar"
			value={80}
			max={100}
			useProgressColour
		/>
	);
	const progressBar = getByTestId("lyzer-progress-bar");
	expect(progressBar).toMatchSnapshot();
});

it("Progress bar renders correctly with 100% value", () => {
	const { getByTestId } = render(
		<Progress 
			testId="lyzer-progress-bar"
			value={100}
			max={100}
			useProgressColour
		/>
	);
	const progressBar = getByTestId("lyzer-progress-bar");
	expect(progressBar).toMatchSnapshot();
});
