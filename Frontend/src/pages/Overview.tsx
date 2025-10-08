import { RefreshCcw } from "lucide-react";
import LyzerError from "../components/Error";
import { GridContainer } from "../components/Grid";
import useOverview from "../hooks/useOverview";
import CardSection from "../layouts/Overview/CardSection";
import TableSection from "../layouts/Overview/TableSection";

export default function Overview() {
	const {
		error,
		isLoading,
		raceWeekendProgress,
		upcomingRaceWeekend,
		seasonProgress,
		refreshData,
	} = useOverview();

	if (error) return <LyzerError error={error}></LyzerError>;

	return (
		<div className="page-content" data-testid="lyzer-overview-page">
			<div className="flex-row justify-between items-center">
				<h2>Overview</h2>
				<RefreshCcw className="cursor-pointer" size={24} onClick={refreshData} />
			</div>
			<GridContainer>
				<CardSection
					loading={isLoading}
					raceWeekendProgress={raceWeekendProgress}
					upcomingRaceWeekend={upcomingRaceWeekend}
				/>
				<TableSection loading={isLoading} seasonProgress={seasonProgress} />
			</GridContainer>
		</div>
	);
}
