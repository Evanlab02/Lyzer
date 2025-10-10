import { PropsWithChildren } from "react";
import SkeletonLoader from "../SkeletonLoader";

export interface CardBodyProps extends PropsWithChildren {
	skeletonHeight?: string;
	skeletonVariant?: "text" | "rect" | "circle";
	loading?: boolean;
}

export default function CardBody(props: Readonly<CardBodyProps>) {
	const { children, skeletonHeight, skeletonVariant, loading = false } = props;
	if (loading) {
		return (
			<h3 className="lyzer-card-body">
				<SkeletonLoader height={skeletonHeight} variant={skeletonVariant} />
			</h3>
		);
	}

	return <h3 className="lyzer-card-body">{children}</h3>;
}
