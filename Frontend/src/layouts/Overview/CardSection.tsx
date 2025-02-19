import Progress from "../../components/Progress";
import Upcoming
import Card, {CardBody, CardHeading} from "../../components/Card";
import { GridItem } from "../../components/Grid";
import { RaceWeekendProgress, UpcomingRaceWeekend } from "../../clients/interfaces/overviewInterfaces";
import { LOADING_RACE_WEEKEND_PROGRESS } from "../../constants/loading";

export interface CardSectionProps {
    raceWeekendProgress?: RaceWeekendProgress;
	upcomingRaceWeekend?: UpcomingRaceWeekend;
}

export default function CardSection(props: CardSectionProps) {
	const { 
		raceWeekendProgress = LOADING_RACE_WEEKEND_PROGRESS
	} = props;

	return (
		<>
			<GridItem xs={12} sm={12} md={6} lg={4} xl={3} xxl={3}>
				<Card>
					<CardHeading>
                        Is it race weekend?
					</CardHeading>
					<CardBody>
						{upcomingRaceWeekend.isRaceWeekend ? "Yes" : "No"}
					</CardBody>

				</Card>
			</GridItem>
			<GridItem xs={12} sm={12} md={6} lg={4} xl={3} xxl={3}>
				<Card>
					<CardHeading>
                        Race weekend progress
					</CardHeading>
					<CardBody>
						{raceWeekendProgress.ongoing ? `Ongoing session: ${raceWeekendProgress.name}` : `Next session: ${raceWeekendProgress.name}`}
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