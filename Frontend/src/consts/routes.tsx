import { Home, User } from "lucide-react"
import { ReactElement } from "react"

interface RouteInterface {
	name: string,
	route: string,
	icon: ReactElement<any, any>;
}

export const ROUTES: { [key: string]: RouteInterface } = {
	OVERVIEW: {
		name: 'overview',
		route: '/',
		icon: <Home />,
	},
	DRIVERS: {
		name: 'drivers',
		route: '/drivers',
		icon: <User />,
	},
}
