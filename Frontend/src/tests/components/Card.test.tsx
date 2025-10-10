import { render } from "@testing-library/react";
import { expect, it } from "vitest";
import Card, { CardBody, CardHeading, CardSection } from "../../components/Card";

it("Basic card renders correctly", async () => {
	const { findByTestId } = render(<Card testId="lyzer-card">Basic Card</Card>);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});

it("Card with heading renders correctly", async () => {
	const { findByTestId } = render(
		<Card testId="lyzer-card">
			<CardHeading>Card Heading</CardHeading>
		</Card>
	);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});

it("Card with body renders correctly", async () => {
	const { findByTestId } = render(
		<Card testId="lyzer-card">
			<CardBody>Card Body</CardBody>
		</Card>
	);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});

it("Card with heading and body renders correctly", async () => {
	const { findByTestId } = render(
		<Card testId="lyzer-card">
			<CardHeading>Card Heading</CardHeading>
			<CardBody>Card Body</CardBody>
		</Card>
	);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});

it("Card with section renders correctly", async () => {
	const { findByTestId } = render(
		<Card testId="lyzer-card">
			<CardSection title="Card Section Title" subtitle="Card Section Subtitle" />
		</Card>
	);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});

it("Card with section renders correctly, including children", async () => {
	const { findByTestId } = render(
		<Card testId="lyzer-card">
			<CardSection title="Card Section Title" subtitle="Card Section Subtitle">
				<p>Card Section Content</p>
			</CardSection>
		</Card>
	);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});

it("Loading card body renders skeleton correctly.", async () => {
	const { findByTestId } = render(
		<Card testId="lyzer-card">
			<CardBody loading={true}>Card Body</CardBody>
		</Card>
	);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});

it("Loading card body renders skeleton correctly with custom height.", async () => {
	const { findByTestId } = render(
		<Card testId="lyzer-card">
			<CardBody loading={true} skeletonHeight="600px">
				Card Body
			</CardBody>
		</Card>
	);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});

it("Loading card body renders skeleton correctly with custom variant.", async () => {
	const { findByTestId } = render(
		<Card testId="lyzer-card">
			<CardBody loading={true} skeletonVariant="circle">
				Card Body
			</CardBody>
		</Card>
	);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});

it("Loading card body renders skeleton correctly with custom variant and height.", async () => {
	const { findByTestId } = render(
		<Card testId="lyzer-card">
			<CardBody loading={true} skeletonVariant="circle" skeletonHeight="600px">
				Card Body
			</CardBody>
		</Card>
	);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});

it("Card with loading section renders correctly.", async () => {
	const { findByTestId } = render(
		<Card testId="lyzer-card">
			<CardSection title="" subtitle="" loading skeletonBodyHeight="600px" skeletonSubtitleHeight="90px" skeletonTitleHeight="80px">
				<p>Card Section Content</p>
			</CardSection>
		</Card>
	);

	const card = await findByTestId("lyzer-card");
	expect(card).toMatchSnapshot();
});