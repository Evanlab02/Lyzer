import { beforeEach, vi } from "vitest";
import createFetchMock from 'vitest-fetch-mock';

const fetchMocker = createFetchMock(vi);
fetchMocker.enableMocks();

beforeEach(() => {
	vi.clearAllMocks();
	document.body.innerHTML = "";

	// Mock matchMedia
	Object.defineProperty(window, "matchMedia", {
		writable: true,
		value: vi.fn().mockImplementation(query => ({
			matches: false,
			// eslint-disable-next-line @typescript-eslint/no-unsafe-assignment
			media: query, 
			onchange: null,
			addListener: vi.fn(),
			removeListener: vi.fn(),
			addEventListener: vi.fn(),
			removeEventListener: vi.fn(),
			dispatchEvent: vi.fn(),
		})),
	});
});