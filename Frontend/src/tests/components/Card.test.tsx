import { expect, it } from "vitest";
import { render } from "@testing-library/react";
import Card, { CardBody, CardHeading } from "../../components/Card";

it("Basic card renders correctly", async () => {
	const { findByTestId } = render(
		<Card testId="lyzer-card">
			Basic Card
		</Card>
	);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});

it("Card with heading renders correctly", async () => {
	const { findByTestId } = render(
		<Card testId="lyzer-card">
			<CardHeading>
                Card Heading
			</CardHeading>
		</Card>
	);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});

it("Card with body renders correctly", async () => {
	const { findByTestId } = render(
		<Card testId="lyzer-card">
			<CardBody>
                Card Body
			</CardBody>
		</Card>
	);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});

it("Card with heading and body renders correctly", async () => {
	const { findByTestId } = render(
		<Card testId="lyzer-card">
			<CardHeading>
                Card Heading
			</CardHeading>
			<CardBody>
                    Card Body
			</CardBody>
		</Card>
	);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});
