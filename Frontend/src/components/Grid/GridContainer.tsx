import { PropsWithChildren } from "react";
import "./styles/GridContainer.scss";

export default function GridContainer(props: Readonly<PropsWithChildren>) {
	const { children } = props;

	return (
		<div className="flex-grid-container">
			{children}
		</div>
	);
}