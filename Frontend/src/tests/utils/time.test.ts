import { expect, it } from "vitest";
import { convertMinutesToHighestDenominator } from "../../utils/time";

it("convertMinutesToHighestDenominator should return the correct for weeks", () => {
	const result = convertMinutesToHighestDenominator(21000);
	expect(result).toBe("2 weeks");
});

it("convertMinutesToHighestDenominator should return the correct for 1 week", () => {
	const result = convertMinutesToHighestDenominator(10080);
	expect(result).toBe("1 week");
});

it("convertMinutesToHighestDenominator should return the correct for days", () => {
	const result = convertMinutesToHighestDenominator(3000);
	expect(result).toBe("2 days");
});

it("convertMinutesToHighestDenominator should return the correct for 1 day", () => {
	const result = convertMinutesToHighestDenominator(1440);
	expect(result).toBe("1 day");
});

it("convertMinutesToHighestDenominator should return the correct for hours", () => {
	const result = convertMinutesToHighestDenominator(180);
	expect(result).toBe("3 hours");
});

it("convertMinutesToHighestDenominator should return the correct for 1 hour", () => {
	const result = convertMinutesToHighestDenominator(60);
	expect(result).toBe("1 hour");
});

it("convertMinutesToHighestDenominator should return the correct for minutes", () => {
	const result = convertMinutesToHighestDenominator(7);
	expect(result).toBe("7 minutes");
});

it("convertMinutesToHighestDenominator should return the correct for 1 minute", () => {
	const result = convertMinutesToHighestDenominator(1);
	expect(result).toBe("1 minute");
});

it("convertMinutesToHighestDenominator should return the correct for 0 minutes", () => {
	const result = convertMinutesToHighestDenominator(0);
	expect(result).toBe("0 minutes");
});

