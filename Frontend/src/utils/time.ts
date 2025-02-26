import { Duration } from "luxon";

export function convertMinutesToHighestDenominator(minutes: number) {
	const duration = Duration.fromObject({ minutes });

	const durationAsWeeks = duration.as("weeks");
	const durationAsDays = duration.as("days");
	const durationAsHours = duration.as("hours");

	if (durationAsWeeks >= 1) {
		const weeks = Math.floor(durationAsWeeks);
		const suffix = weeks > 1 ? "weeks" : "week";
		return `${weeks.toString()} ${suffix}`;
	}

	if (durationAsDays >= 1) {
		const days = Math.floor(durationAsDays);
		const suffix = days > 1 ? "days" : "day";
		return `${days.toString()} ${suffix}`;
	}

	if (durationAsHours >= 1) {
		const hours = Math.floor(durationAsHours);
		const suffix = hours > 1 ? "hours" : "hour";
		return `${hours.toString()} ${suffix}`;
	}

    
	const minutesFloored = Math.floor(minutes);
	const suffix = minutesFloored > 1 ? "minutes" : "minute";
	return `${minutesFloored.toString()} ${suffix}`;
}
