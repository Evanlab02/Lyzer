import React, { useEffect, useRef } from "react";
import { Link, useLocation } from "react-router-dom";
import { ArrowLeftToLine } from "lucide-react";
import { ROUTES } from "../../constants/routes";
import { useTheme } from "../../hooks/useTheme";
import "./styles/index.scss";

interface SideNavProps  {
	sideNavOpen: boolean;
	testId?: string;
	menuToggleTestId?: string;
	onCloseClick: () => void;
	onSideNavBlur: React.FocusEventHandler<SVGSVGElement>;
}

export default function SideMenu(props: Readonly<SideNavProps>) {
	const { 
		sideNavOpen, 
		testId,
		menuToggleTestId,
		onCloseClick, 
		onSideNavBlur
	} = props;

	const {isDarkMode} = useTheme();

	const sideNavRef = useRef<HTMLDivElement>(null);

	const location = useLocation();

	useEffect(() => {
		function handleOutsideClick(event: MouseEvent) {
			if (sideNavRef.current && !sideNavRef.current.contains(event.target as Node)) {
				onCloseClick();
			}
		}

		if (sideNavOpen) {
			document.addEventListener("mousedown", handleOutsideClick);
		}

		return () => {
			document.removeEventListener("mousedown", handleOutsideClick);
		};
	}, [sideNavOpen, onCloseClick]);

	const isSelectedRoute: (route: string) => boolean = (route: string) => {
		return location.pathname === route;
	};

	return (
		<div id="side-menu" className={"side-nav " + (sideNavOpen ? "open" : "closed")} ref={sideNavRef} data-testid={testId}>
			<ArrowLeftToLine 
				onClick={onCloseClick}
				onBlur={onSideNavBlur}
				color={isDarkMode ? "white" : "black"}
				className="closeIcon"
				size={28}
				data-testid={menuToggleTestId}
			/>
			<div id="links" className="links">
				{/* Check if active link, apply active style
				Create hover style
				Add logo to top of side menu */}
				{Object.values(ROUTES).map((item, index) => (
					<Link 
						key={`linkContainer${index.toString()}`} 
						className={"link-container " + (isSelectedRoute(item.route) ? "selected" : "")}
						to={item.route}
					>
						{item.children}
						<div key={`linkName${index.toString()}`}>
							{item.name.toUpperCase()}
						</div>
					</Link>
				))}
			</div>
		</div>
	);
}