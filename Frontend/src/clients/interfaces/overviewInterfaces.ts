import z from "zod";

export const RaceWeekendProgressSchema = z.object({
	name: z.string(),
	ongoing: z.boolean(),
	weekendProgress: z.number(),
	startDateTime: z.string().optional(),
});

export const UpcomingRaceWeekendSchema = z.object({
	isRaceWeekend: z.boolean(),
	timeToRaceWeekendProgress: z.number(),
	timeToRaceWeekend: z.number(),
	status: z.string(),
});

export const SeasonProgressSchema = z.object({
	previousRaceWinner: z.string(),
	previousGrandPrix: z.string(),
	seasonProgress: z.number(),
	seasonTotalRaces: z.number(),
});

export const DriverStandingsEntrySchema = z.object({
	position: z.string(),
	driver: z.string(),
	points: z.string(),
	color: z.string(),
});

export const DriverStandingsSchema = z.object({
	leader: z.string(),
	color: z.string(),
	standings: z.array(DriverStandingsEntrySchema),
});

export const OverviewDataSchema = z.object({
	raceWeekendProgress: RaceWeekendProgressSchema,
	upcomingRaceWeekend: UpcomingRaceWeekendSchema,
	seasonProgress: SeasonProgressSchema,
	drivers: DriverStandingsSchema,
});

export type RaceWeekendProgress = z.infer<typeof RaceWeekendProgressSchema>;
export type UpcomingRaceWeekend = z.infer<typeof UpcomingRaceWeekendSchema>;
export type SeasonProgress = z.infer<typeof SeasonProgressSchema>;
export type DriverStandingsEntry = z.infer<typeof DriverStandingsEntrySchema>;
export type DriverStandings = z.infer<typeof DriverStandingsSchema>;
export type OverviewData = z.infer<typeof OverviewDataSchema>;
