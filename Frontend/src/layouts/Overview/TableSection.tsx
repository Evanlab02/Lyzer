import { useMemo } from "react";
import { SeasonProgress } from "../../clients/interfaces/overviewInterfaces";
import Card, { CardBody, CardHeading, CardSection } from "../../components/Card";
import { GridItem } from "../../components/Grid";

export interface TableSectionProps {
	loading: boolean;
	seasonProgress: SeasonProgress;
}

export default function TableSection(props: Readonly<TableSectionProps>) {
	const { loading, seasonProgress } = props;

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
							skeletonTitleHeight="20"
							skeletonSubtitleHeight="10"
							loading={loading}
						/>
						<CardSection
							title={previousGrandPrix}
							subtitle="Previous Grand Prix"
							skeletonTitleHeight="20"
							skeletonSubtitleHeight="10"
							loading={loading}
						/>
						<CardSection
							title={seasonProgressDisplay}
							subtitle="Season Progress"
							skeletonTitleHeight="20"
							skeletonSubtitleHeight="10"
							loading={loading}
						/>
					</CardBody>
				</Card>
			</GridItem>
			<GridItem xs={12} sm={12} md={12} lg={12} xl={9} xxl={9}>
				<Card>
					<CardHeading>Constructor Standings</CardHeading>
				</Card>
			</GridItem>
			<GridItem xs={12} sm={12} md={12} lg={12} xl={12} xxl={12}>
				<Card>
					<CardHeading>Driver Standings</CardHeading>
				</Card>
			</GridItem>
		</>
	);
}
