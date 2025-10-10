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
		drivers,
		fetchData,
	} = useOverview();

	if (error) return <LyzerError error={error}></LyzerError>;

	const { leader, color, standings } = drivers;

	return (
		<div className="page-content" data-testid="lyzer-overview-page">
			<div className="flex-row justify-between items-center">
				<h2>Overview</h2>
				<RefreshCcw
					className="cursor-pointer"
					size={24}
					onClick={() => {
						void fetchData();
					}}
				/>
			</div>
			<GridContainer>
				<CardSection
					loading={isLoading}
					raceWeekendProgress={raceWeekendProgress}
					upcomingRaceWeekend={upcomingRaceWeekend}
					leader={leader}
					leaderColor={color}
				/>
				<TableSection
					loading={isLoading}
					seasonProgress={seasonProgress}
					driverStandings={standings}
				/>
			</GridContainer>
		</div>
	);
}
