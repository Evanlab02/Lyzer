import { render } from "@testing-library/react";
import { expect, it } from "vitest";
import LyzerError from "../../components/Error";

it("Empty lyzer error", async () => {
	const { findByTestId } = render(<LyzerError testId="lyzer-error"/>);

	const error = await findByTestId("lyzer-error");
	expect(error).toMatchSnapshot();
});

it("Lyzer error with message", async () => {
	const { findByTestId } = render(<LyzerError testId="lyzer-error" message="Test Custom Error"/>);

	const error = await findByTestId("lyzer-error");
	expect(error).toMatchSnapshot();
});

it("Lyzer error with error obj", async () => {
	const { findByTestId } = render(<LyzerError testId="lyzer-error" error={new Error("Test Custom Error Obj")}/>);

	const error = await findByTestId("lyzer-error");
	expect(error).toMatchSnapshot();
});