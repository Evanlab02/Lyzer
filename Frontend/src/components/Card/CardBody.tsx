import { PropsWithChildren } from "react";

export default function CardBody(props: Readonly<PropsWithChildren>) {
	const { children } = props;

	return (
		<h3 className="lyzer-card-body">
			{children}
		</h3>
	);
}