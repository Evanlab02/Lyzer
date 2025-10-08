import "./styles/index.scss";

export interface SkeletonLoaderProps {
	width?: string;
	height?: string;
	variant?: "text" | "rect" | "circle";
	className?: string;
}

export default function SkeletonLoader(props: Readonly<SkeletonLoaderProps>) {
	const { width = "100%", height = "20px", variant = "rect", className = "" } = props;

	const style = {
		width,
		height,
	};

	return (
		<div
			className={`skeleton-loader skeleton-loader--${variant} ${className}`}
			style={style}
		/>
	);
}
