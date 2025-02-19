import { Route, Routes } from "react-router-dom";
import Overview from "../pages/Overview";
import NotFound from "../pages/404";

export default function Navigation() {
	return (
		<Routes>
			<Route path="" element={<Overview/>} />
			<Route path="*" element={<NotFound/>} />
		</Routes>
	);
};
