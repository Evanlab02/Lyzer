import { DateTime } from "luxon";
import Progress from "../../components/Progress";
import Card, {CardBody, CardHeading} from "../../components/Card";
import { GridItem } from "../../components/Grid";
import { RaceWeekendProgress, UpcomingRaceWeekend } from "../../clients/interfaces/overviewInterfaces";
import { LOADING_RACE_WEEKEND_PROGRESS, LOADING_UPCOMING_RACE_WEEKEND } from "../../constants/loading";
import { convertMinutesToHighestDenominator } from "../../utils/time";


export interface CardSectionProps {
    raceWeekendProgress?: RaceWeekendProgress;
	upcomingRaceWeekend?: UpcomingRaceWeekend;
}

export default function CardSection(props: Readonly<CardSectionProps>) {
	const { 
		raceWeekendProgress = LOADING_RACE_WEEKEND_PROGRESS,
		upcomingRaceWeekend = LOADING_UPCOMING_RACE_WEEKEND
	} = props;

	return (
		<>
			<GridItem xs={12} sm={12} md={6} lg={4} xl={3} xxl={3}>
				<Card>
					<CardHeading>
                        Is it race weekend?
					</CardHeading>
					<CardBody>
						{upcomingRaceWeekend.status}
						{upcomingRaceWeekend.timeToRaceWeekend > 0 && !upcomingRaceWeekend.isRaceWeekend && (
							<p className="text-sm m-0">
								{`${convertMinutesToHighestDenominator(upcomingRaceWeekend.timeToRaceWeekend)} to go`}
							</p>
						)}
					</CardBody>
					<Progress 
						value={upcomingRaceWeekend.timeToRaceWeekendProgress}
						max={100}
						useProgressColour
					/>
				</Card>
			</GridItem>
			<GridItem xs={12} sm={12} md={6} lg={4} xl={3} xxl={3}>
				<Card>
					<CardHeading>
                        Race weekend progress
					</CardHeading>
					<CardBody>
						{raceWeekendProgress.ongoing ? `Ongoing session: ${raceWeekendProgress.name}` : `Next session: ${raceWeekendProgress.name}`}
						{raceWeekendProgress.startDateTime && (
							<p className="text-sm m-0">
								{DateTime.fromISO(raceWeekendProgress.startDateTime).toLocaleString(DateTime.DATETIME_MED_WITH_WEEKDAY)}
							</p>
						)}
					</CardBody>
					<Progress 
						value={raceWeekendProgress.weekendProgress}
						max={100}
						useProgressColour
					/>
				</Card>
			</GridItem>
			<GridItem xs={12} sm={12} md={6} lg={4} xl={3} xxl={3}>
				<Card>
					<CardHeading>
                        Driver leader
					</CardHeading>
				</Card>
			</GridItem>
			<GridItem xs={12} sm={12} md={6} lg={4} xl={3} xxl={3}>
				<Card>
					<CardHeading>
                        Constructor leader
					</CardHeading>
				</Card>
			</GridItem>
		</>
	);
}