import { PropsWithChildren } from "react";

export interface CardSectionProps extends PropsWithChildren {
    title: string;
    subtitle: string;

}

export default function CardSection(props: Readonly<CardSectionProps>) {
	const { title, subtitle, children } = props;

	return (
		<div className="lyzer-card-section">
			<h3 className="lyzer-card-section-title">{title}</h3>
			<p className="lyzer-card-section-subtitle">{subtitle}</p>
			{children}
			<hr className="lyzer-card-section-divider" />
		</div>
	);
}