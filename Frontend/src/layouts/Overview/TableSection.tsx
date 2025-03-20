import { useMemo } from "react";
import { SeasonProgress } from "../../clients/interfaces/overviewInterfaces";
import Card, { CardBody, CardHeading, CardSection } from "../../components/Card";
import { GridItem } from "../../components/Grid";
import { LOADING_SEASON_PROGRESS } from "../../constants/loading";
export interface TableSectionProps {
	seasonProgress?: SeasonProgress;
}

export default function TableSection(props: Readonly<TableSectionProps>) {
	const { 
		seasonProgress = LOADING_SEASON_PROGRESS
	} = props;

	const previousRaceWinner = seasonProgress.previousRaceWinner;
	const previousGrandPrix = seasonProgress.previousGrandPrix;

	const seasonProgressDisplay = useMemo(() => {
		return seasonProgress.seasonProgress === 0 && seasonProgress.seasonTotalRaces === 0 
			? "Loading..." 
			: `${seasonProgress.seasonProgress.toString()} of ${seasonProgress.seasonTotalRaces.toString()} races`;
	}, [seasonProgress.seasonProgress, seasonProgress.seasonTotalRaces]);

	return (
		<>
			<GridItem xs={12} sm={12} md={12} lg={12} xl={3} xxl={3}>
				<Card>
					<CardBody>
						<CardSection
							title={previousRaceWinner}
							subtitle="Previous Race Winner"
						/>
						<CardSection
							title={previousGrandPrix}
							subtitle="Previous Grand Prix"
						/>
						<CardSection
							title={seasonProgressDisplay}
							subtitle="Season Progress"
						/>
					</CardBody>
				</Card>
			</GridItem>
			<GridItem xs={12} sm={12} md={12} lg={12} xl={9} xxl={9}>
				<Card>
					<CardHeading>
						Constructor Standings
					</CardHeading>
				</Card>
			</GridItem>
			<GridItem xs={12} sm={12} md={12} lg={12} xl={12} xxl={12}>
				<Card>
					<CardHeading>
						Driver Standings
					</CardHeading>
				</Card>
			</GridItem>
		</>
	);
}