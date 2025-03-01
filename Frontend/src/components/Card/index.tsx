import CardBody from "./CardBody";
import CardHeading from "./CardHeading";
import { PropsWithChildren } from "react";
import "./styles/index.scss";

export interface CardProps extends PropsWithChildren {
    testId?: string;
}

export default function Card(props: CardProps) {
	const { children, testId } = props;

	return(
		<div className="lyzer-card" data-testid={testId}>
			{children}
		</div>
	);
}

export { CardBody, CardHeading };
