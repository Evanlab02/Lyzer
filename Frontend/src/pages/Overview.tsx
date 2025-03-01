import { GridContainer } from "../components/Grid";
import CardSection from "../layouts/Overview/CardSection";
import TableSection from "../layouts/Overview/TableSection";
import useOverview from "../hooks/useOverview";
import { RefreshCcw } from "lucide-react";

export default function Overview() {
	const { 
		raceWeekendProgress,
		upcomingRaceWeekend,
		refreshData
	} = useOverview();;

	return (
		<div className="page-content" data-testid="lyzer-overview-page">
			<div className="flex-row justify-between items-center">
				<h2>Overview</h2>
				<RefreshCcw 
					className="cursor-pointer" 
					size={24} 
					onClick={refreshData}
				/>
			</div>
			<GridContainer>
				<CardSection 
					raceWeekendProgress={raceWeekendProgress}
					upcomingRaceWeekend={upcomingRaceWeekend}
				/>
				<TableSection />
			</GridContainer>
		</div>
	);
};