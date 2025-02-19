import { vi, expect, it } from "vitest";
import { MemoryRouter } from "react-router-dom";
import { fireEvent, render } from "@testing-library/react";
import ThemeProvider from "../../components/ThemeProvider";
import SideMenu from "../../components/SideNav";


it("SideNav renders closed correctly", () => {
	const { getByTestId } = render(
		<MemoryRouter initialEntries={["/"]}>
			<ThemeProvider>
				<SideMenu
					onCloseClick={vi.fn()}
					onSideNavBlur={vi.fn()}
					sideNavOpen={false}
					testId="lyzer-side-nav"
				/>
			</ThemeProvider>
		</MemoryRouter>
	);

	const sideNav = getByTestId("lyzer-side-nav");
	expect(sideNav).toMatchSnapshot();
});

it("SideNav renders open correctly", () => {
	const { getByTestId } = render(
		<MemoryRouter initialEntries={["/"]}>
			<ThemeProvider>
				<SideMenu
					onCloseClick={vi.fn()}
					onSideNavBlur={vi.fn()}
					sideNavOpen={true}
					testId="lyzer-side-nav"
				/>
			</ThemeProvider>
		</MemoryRouter>
	);

	const sideNav = getByTestId("lyzer-side-nav");
	expect(sideNav).toMatchSnapshot();
});

it("SideNav calls onCloseClick when menuToggle is clicked", () => {
	const onCloseClick = vi.fn();
	const { getByTestId } = render(
		<MemoryRouter initialEntries={["/"]}>
			<ThemeProvider>
				<SideMenu
					onCloseClick={onCloseClick}
					onSideNavBlur={vi.fn()}
					sideNavOpen={true}
					testId="lyzer-side-nav"
					menuToggleTestId="lyzer-side-nav-menu-toggle"
				/>
			</ThemeProvider>
		</MemoryRouter>
	);

	const menuToggle = getByTestId("lyzer-side-nav-menu-toggle");
	fireEvent.click(menuToggle);

	expect(onCloseClick).toHaveBeenCalledTimes(1);
});
