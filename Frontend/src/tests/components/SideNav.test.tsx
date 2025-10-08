import { fireEvent, render } from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import { expect, it, vi } from "vitest";
import SideMenu from "../../components/SideNav";
import ThemeProvider from "../../components/ThemeProvider";

it("SideNav renders closed correctly", async () => {
	const { findByTestId } = render(
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

	const sideNav = await findByTestId("lyzer-side-nav");
	expect(sideNav).toMatchSnapshot();
});

it("SideNav renders open correctly", async () => {
	const { findByTestId } = render(
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

	const sideNav = await findByTestId("lyzer-side-nav");
	expect(sideNav).toMatchSnapshot();
});

it("SideNav calls onCloseClick when menuToggle is clicked", async () => {
	const onCloseClick = vi.fn();
	const { findByTestId } = render(
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

	const menuToggle = await findByTestId("lyzer-side-nav-menu-toggle");
	fireEvent.click(menuToggle);

	expect(onCloseClick).toHaveBeenCalledTimes(1);
});
