import { Home, User } from "lucide-react";
import { ReactElement } from "react";

interface RouteInterface {
	name: string,
	route: string,
	/* eslint-disable @typescript-eslint/no-explicit-any */
	icon: ReactElement<any, any>;
}

export const ROUTES: Record<string, RouteInterface> = {
	OVERVIEW: {
		name: "overview",
		route: "/",
		icon: <Home />,
	},
	DRIVERS: {
		name: "drivers",
		route: "/drivers",
		icon: <User />,
	},
};
