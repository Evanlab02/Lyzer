import z from "zod";

export function buildFetchOptions(method: "GET") {
	return {
		method: method,
		headers: {
			"Content-Type": "application/json",
		},
	};
}

export async function handleFetchResponse<T>(
	response: Response,
	error: string,
	schema: z.ZodType<T>
) {
	if (!response.ok || response.status !== 200) {
		let message = "No additional information";
		try {
			const jsonContent = (await response.json()) as unknown;
			if (
				jsonContent &&
				typeof jsonContent === "object" &&
				"message" in jsonContent &&
				typeof jsonContent.message === "string"
			) {
				message = jsonContent.message;
			}
		} catch {
			console.warn("Failed to parse JSON, ignoring.");
		}
		throw new Error(`${error} (${response.status.toString()} - ${message})`);
	}

	return schema.parse(await response.json());
}
