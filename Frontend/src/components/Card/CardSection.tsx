import { PropsWithChildren } from "react";
import SkeletonLoader from "../SkeletonLoader";

export interface CardSectionProps extends PropsWithChildren {
	title: string;
	subtitle: string;
	skeletonTitleHeight?: string;
	skeletonSubtitleHeight?: string;
	skeletonBodyHeight?: string;
	skeletonBodyVariant?: "text" | "rect" | "circle";
	loading?: boolean;
}

export default function CardSection(props: Readonly<CardSectionProps>) {
	const {
		title,
		subtitle,
		children,
		skeletonTitleHeight,
		skeletonSubtitleHeight,
		skeletonBodyHeight,
		skeletonBodyVariant,
		loading = false,
	} = props;

	if (loading) {
		return (
			<div className="lyzer-card-section">
				<h3 className="lyzer-card-section-title">
					<SkeletonLoader height={skeletonTitleHeight} variant="text" />
				</h3>
				{skeletonSubtitleHeight && (
					<SkeletonLoader
						className="lyzer-card-section-subtitle"
						height={skeletonSubtitleHeight}
						variant="text"
					/>
				)}
				{skeletonBodyHeight && (
					<SkeletonLoader height={skeletonBodyHeight} variant={skeletonBodyVariant} />
				)}
				<hr className="lyzer-card-section-divider" />
			</div>
		);
	}

	return (
		<div className="lyzer-card-section">
			<h3 className="lyzer-card-section-title">{title}</h3>
			<p className="lyzer-card-section-subtitle">{subtitle}</p>
			{children}
			<hr className="lyzer-card-section-divider" />
		</div>
	);
}
