import { useMemo } from "react";
import { DriverStandingsEntry, SeasonProgress } from "../../clients/interfaces/overviewInterfaces";
import Card, { CardBody, CardHeading, CardSection } from "../../components/Card";
import { GridItem } from "../../components/Grid";
import Table, { TableColumn, TableRow } from "../../components/Table";

const DRIVER_COLUMNS: TableColumn[] = [
	{
		display: "Pos",
		width: "70px",
	},
	{
		display: "Driver",
	},
	{
		display: "Points",
		width: "100px",
	},
	{
		display: "Gap",
		width: "100px",
		vanish: true,
	},
	{
		display: "To Leader",
		width: "100px",
		vanish: true,
	},
];

export interface TableSectionProps {
	loading: boolean;
	seasonProgress: SeasonProgress;
	driverStandings: DriverStandingsEntry[];
}

export default function TableSection(props: Readonly<TableSectionProps>) {
	const { loading, seasonProgress, driverStandings } = props;
	const { previousRaceWinner, previousGrandPrix } = seasonProgress;
	const seasonProgressDisplay = `${seasonProgress.seasonProgress.toString()} of ${seasonProgress.seasonTotalRaces.toString()} races`;

	const driverRows = useMemo(() => {
		if (driverStandings.length === 0) return [];

		const leaderPoints = parseFloat(driverStandings[0].points);

		return driverStandings.map((data, index) => {
			const currentPoints = parseFloat(data.points);

			// Calculate gap to previous driver
			let gap: string | number = "-";
			if (index > 0) {
				const previousPoints = parseFloat(driverStandings[index - 1].points);
				gap = previousPoints - currentPoints;
			}

			// Calculate gap to leader
			let gapToLeader: string | number = "-";
			if (index > 0) {
				gapToLeader = leaderPoints - currentPoints;
			}

			const driverNameArray = data.driver.split(" ");
			const driverTag = driverNameArray[driverNameArray.length - 1].slice(0, 3).toUpperCase();

			return {
				color: data.color,
				values: [
					{ value: data.position, fontWeight: "700", width: "70px" },
					{ value: data.driver, altValue: driverTag, fontWeight: "500" },
					{ value: data.points, fontWeight: "700", width: "100px" },
					{
						value: gap,
						fontWeight: "600",
						width: "100px",
						customClass: "off-text-color",
						vanish: true,
					},
					{
						value: gapToLeader,
						fontWeight: "600",
						width: "100px",
						customClass: "off-text-color",
						vanish: true,
					},
				],
			} as TableRow;
		});
	}, [driverStandings]);

	return (
		<>
			<GridItem xs={12} sm={12} md={12} lg={12} xl={3} xxl={3}>
				<Card>
					<CardHeading>Season Information</CardHeading>
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
					<CardBody></CardBody>
				</Card>
			</GridItem>
			<GridItem xs={12} sm={12} md={12} lg={12} xl={12} xxl={12}>
				<Card>
					<CardHeading>Driver Standings</CardHeading>
					<CardBody>
						<Table columns={DRIVER_COLUMNS} rows={driverRows} loading={loading} loadingRows={20} />
					</CardBody>
				</Card>
			</GridItem>
		</>
	);
}
