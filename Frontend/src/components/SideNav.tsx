import { ArrowLeftToLine } from "lucide-react";
import "../styles/sideNav.scss"
import { useTheme } from "../hooks/useTheme";
import React, { useEffect, useRef } from "react";
import { ROUTES } from "../consts/routes";
import { Link, useLocation } from "react-router-dom";

type SideNavProps = {
	sideNavOpen: boolean;
	onCloseClick: () => void;
	onSideNavBlur: React.FocusEventHandler<SVGSVGElement>;
}

export default function SideMenu({ sideNavOpen, onCloseClick, onSideNavBlur}: SideNavProps) {

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
		<div id="side-menu" className={"side-nav " + (sideNavOpen ? "open" : "closed")} ref={sideNavRef}>
			<ArrowLeftToLine 
				onClick={onCloseClick}
				onBlur={onSideNavBlur}
				color={isDarkMode ? "white" : "black"}
				className="closeIcon"
				size={28}
			/>
			<div id="links" className="links">
				{/* Check if active link, apply active style
				Create hover style
				Add logo to top of side menu */}
				{Object.values(ROUTES).map((item, index) => (
					<Link 
						key={"linkContainer" + index} 
						className={"link-container " + (isSelectedRoute(item.route) ? 'selected' : '')}
						to={item.route}
					>
						{item.icon}
						<div key={"linkName" + index}>
							{item.name.toUpperCase()}
						</div>
					</Link>
				))}
			</div>
		</div>
	)
}