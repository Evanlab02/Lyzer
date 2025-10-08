import { AlertCircle } from "lucide-react";
import "./styles/index.scss";

export interface ErrorProps {
	error?: Error;
	message?: string;
	testId?: string;
}

export default function LyzerError(props: Readonly<ErrorProps>) {
	const { error, message, testId } = props;

	const errorMessage = message ?? error?.message ?? "An unexpected error occurred";

	return (
		<div className="lyzer-error" data-testid={testId}>
			<AlertCircle className="lyzer-error-icon" size={24} />
			<span className="lyzer-error-message">{errorMessage}</span>
		</div>
	);
}
