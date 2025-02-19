import { PropsWithChildren } from "react";
import "./styles/GridContainer.scss";

export interface GridContainerProps extends PropsWithChildren {
	testId?: string;
}

export default function GridContainer(props: Readonly<GridContainerProps>) {
	const { children, testId } = props;

	return (
		<div className="flex-grid-container" data-testid={testId}>
			{children}
		</div>
	);
}