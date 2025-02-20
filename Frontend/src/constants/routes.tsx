import { PropsWithChildren } from "react";
import { Home, User } from "lucide-react";

interface RouteInterface extends PropsWithChildren {
	name: string,
	route: string,
}

export const ROUTES: Record<string, RouteInterface> = {
	OVERVIEW: {
		name: "overview",
		route: "/",
		children: <Home />,
	},
	DRIVERS: {
		name: "drivers",
		route: "/drivers",
		children: <User />,
	},
};
