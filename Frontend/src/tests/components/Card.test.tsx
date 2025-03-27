import { expect, it } from "vitest";
import { render } from "@testing-library/react";
import Card, { CardBody, CardHeading, CardSection } from "../../components/Card";

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

it("Card with section renders correctly", async () => {
	const { findByTestId } = render(
		<Card testId="lyzer-card">
			<CardSection
				title="Card Section Title"
				subtitle="Card Section Subtitle"
			/>
		</Card>
	);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});

it("Card with section renders correctly, including children", async () => {
	const { findByTestId } = render(
		<Card testId="lyzer-card">
			<CardSection
				title="Card Section Title"
				subtitle="Card Section Subtitle"
			>
				<p>Card Section Content</p>
			</CardSection>
		</Card>
	);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});
