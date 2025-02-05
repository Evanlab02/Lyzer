import { GridContainer } from "../components/Grid";
import CardSection from "../layouts/Overview/CardSection";
import TableSection from "../layouts/Overview/TableSection";
import useOverview from "../hooks/useOverview";

export default function Overview() {
	const { raceWeekendProgress } = useOverview();

	return (
		<div className="page-content" data-testid="lyzer-overview-page">
			<h2>Overview</h2>
			<GridContainer>
				<CardSection 
					raceWeekendProgress={raceWeekendProgress} 
				/>
				<TableSection />
			</GridContainer>
		</div>
	);
};