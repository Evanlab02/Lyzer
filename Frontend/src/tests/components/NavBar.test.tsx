import { vi, expect, it } from "vitest";
import { fireEvent, render } from "@testing-library/react";
import ThemeProvider from "../../components/ThemeProvider";
import NavBar from "../../components/NavBar";

it("Navbar renders correctly", () => {
	const { getByTestId } = render(
		<ThemeProvider>
			<NavBar
				testId="lyzer-nav-bar"
				onMenuClick={vi.fn()}
			/>
		</ThemeProvider>
	);

	const navBar = getByTestId("lyzer-nav-bar");
	expect(navBar).toMatchSnapshot();
});

it("Navbar onMenuClick is called", () => {
	const onMenuClick = vi.fn();
    
	const { getByTestId } = render(
		<ThemeProvider>
			<NavBar
				testId="lyzer-nav-bar"
				menuToggleTestId="lyzer-nav-bar-menu-icon"
				onMenuClick={onMenuClick}
			/>
		</ThemeProvider>
	);

	const menuIcon = getByTestId("lyzer-nav-bar-menu-icon");
	fireEvent.click(menuIcon);

	expect(onMenuClick).toHaveBeenCalledTimes(1);
});
