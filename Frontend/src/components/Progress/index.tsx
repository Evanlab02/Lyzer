import "./styles/index.scss";

export interface ProgressProps {
	value?: number;
	max?: number;
	useProgressColour?: boolean;
	testId?: string;
	colour?: string;
}

function getProgressClassName(value: number, max: number, useProgressColour: boolean): string {
	if (!useProgressColour) return "";

	const progressPercentage = (value / max) * 100;

	if (progressPercentage >= 100) {
		return "lyzer-progress-complete";
	}
	if (progressPercentage >= 80) {
		return "lyzer-progress-almost";
	}
	return "lyzer-progress-incomplete";
}

export default function Progress(props: Readonly<ProgressProps>) {
	const { value = 100, max = 100, useProgressColour = false, testId, colour } = props;
	const progressClassName = getProgressClassName(value, max, useProgressColour);
	const shadowClassName = value ? "lyzer-progress-shadow" : "";
	const progressStyles = colour
		? ({ "--progress-bar-indicator-colour": colour } as React.CSSProperties)
		: undefined;

	return (
		<progress
			className={`lyzer-progress ${progressClassName} ${shadowClassName}`}
			value={value}
			max={max}
			data-testid={testId}
			style={progressStyles}
		/>
	);
}
