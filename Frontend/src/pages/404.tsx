import LyzerError from "../components/Error";

export default function NotFound() {
	return (
		<div data-testid="lyzer-not-found-page">
			<LyzerError message="Page not found." />
		</div>
	);
}
