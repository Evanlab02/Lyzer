import { DateTime } from "luxon";
import { memo } from "react";
import {
	RaceWeekendProgress,
	UpcomingRaceWeekend,
} from "../../clients/interfaces/overviewInterfaces";
import Card, { CardBody, CardHeading } from "../../components/Card";
import { GridItem } from "../../components/Grid";
import Progress from "../../components/Progress";
import { convertMinutesToHighestDenominator } from "../../utils/time";

interface TimeToRaceWeekendCardContentProps {
	status: string;
	timeToRaceWeekend: number;
	isRaceWeekend: boolean;
}

interface RaceWeekendProgressCardContentProps {
	ongoing: boolean;
	name: string;
	startDateTime?: string;
}

export interface CardSectionProps {
	loading: boolean;
	raceWeekendProgress: RaceWeekendProgress;
	upcomingRaceWeekend: UpcomingRaceWeekend;
	leader: string;
	leaderColor: string;
}

const TimeToRaceWeekendCardContent = memo(function TimeToRaceWeekendCardContent(
	props: TimeToRaceWeekendCardContentProps
) {
	const { status, timeToRaceWeekend, isRaceWeekend } = props;
	if (timeToRaceWeekend < 0 || isRaceWeekend) return status;

	return (
		<>
			{status}
			<p className="text-sm m-0">
				{`${convertMinutesToHighestDenominator(timeToRaceWeekend)} to go`}
			</p>
		</>
	);
});

const RaceWeeekendProgressCardContent = memo(function RaceWeekendProgressCardContent(
	props: RaceWeekendProgressCardContentProps
) {
	const { ongoing, name, startDateTime } = props;
	const fallbackContent = ongoing ? `Ongoing session: ${name}` : `Next session: ${name}`;
	if (!startDateTime) return fallbackContent;

	return (
		<>
			{fallbackContent}
			<p className="text-sm m-0">
				{DateTime.fromISO(startDateTime).toLocaleString(DateTime.DATETIME_MED_WITH_WEEKDAY)}
			</p>
		</>
	);
});

export default function CardSection(props: Readonly<CardSectionProps>) {
	const { loading, raceWeekendProgress, upcomingRaceWeekend, leader, leaderColor } = props;
	const { ongoing, name, weekendProgress, startDateTime } = raceWeekendProgress;
	const { status, timeToRaceWeekend, isRaceWeekend, timeToRaceWeekendProgress } =
		upcomingRaceWeekend;

	return (
		<>
			<GridItem xs={12} sm={12} md={6} lg={6} xl={3} xxl={3}>
				<Card>
					<CardHeading>Is it race weekend?</CardHeading>
					<CardBody loading={loading} skeletonHeight="43px">
						<TimeToRaceWeekendCardContent
							status={status}
							timeToRaceWeekend={timeToRaceWeekend}
							isRaceWeekend={isRaceWeekend}
						/>
					</CardBody>
					<Progress value={timeToRaceWeekendProgress} max={100} useProgressColour />
				</Card>
			</GridItem>
			<GridItem xs={12} sm={12} md={6} lg={6} xl={3} xxl={3}>
				<Card>
					<CardHeading>Race weekend progress</CardHeading>
					<CardBody loading={loading} skeletonHeight="43px">
						<RaceWeeekendProgressCardContent
							ongoing={ongoing}
							name={name}
							startDateTime={startDateTime}
						/>
					</CardBody>
					<Progress value={weekendProgress} max={100} useProgressColour />
				</Card>
			</GridItem>
			<GridItem xs={12} sm={12} md={6} lg={6} xl={3} xxl={3}>
				<Card>
					<CardHeading>Driver leader</CardHeading>
					<CardBody loading={loading} skeletonHeight="43px">
						{leader}
					</CardBody>
					<Progress value={leaderColor ? 100 : 0} colour={leaderColor} />
				</Card>
			</GridItem>
			<GridItem xs={12} sm={12} md={6} lg={6} xl={3} xxl={3}>
				<Card>
					<CardHeading>Constructor leader</CardHeading>
					<CardBody loading={loading} skeletonHeight="43px"></CardBody>
					<Progress value={0} />
				</Card>
			</GridItem>
		</>
	);
}
