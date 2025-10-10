import { Route, Routes } from "react-router-dom";
import NotFound from "../pages/404";
import Overview from "../pages/Overview";

export default function Navigation() {
	return (
		<Routes>
			<Route path="" element={<Overview />} />
			<Route path="*" element={<NotFound />} />
		</Routes>
	);
}
