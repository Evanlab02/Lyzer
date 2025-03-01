import { Duration } from "luxon";

export function convertMinutesToHighestDenominator(minutes: number) {
	const duration = Duration.fromObject({ minutes });

	const durationAsWeeks = duration.as("weeks");
	const durationAsDays = duration.as("days");
	const durationAsHours = duration.as("hours");

	if (durationAsWeeks >= 1) {
		const weeks = Math.round(durationAsWeeks);
		const suffix = weeks > 1 ? "weeks" : "week";
		return `${weeks.toString()} ${suffix}`;
	}

	if (durationAsDays >= 1) {
		const days = Math.round(durationAsDays);
		const suffix = days > 1 ? "days" : "day";
		return `${days.toString()} ${suffix}`;
	}

	if (durationAsHours >= 1) {
		const hours = Math.round(durationAsHours);
		const suffix = hours > 1 ? "hours" : "hour";
		return `${hours.toString()} ${suffix}`;
	}

    
	const minutesFloored = Math.round(minutes);
	const suffix = minutesFloored > 1 || minutesFloored === 0 ? "minutes" : "minute";
	return `${minutesFloored.toString()} ${suffix}`;
}
