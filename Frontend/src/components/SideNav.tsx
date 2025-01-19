import { ArrowLeftToLine, Home } from "lucide-react";
import "../styles/sideNav.scss"
import { useTheme } from "../hooks/useTheme";
import React, { useEffect, useRef } from "react";

type SideNavProps = {
	sideNavOpen: boolean;
	onCloseClick: () => void;
	onSideNavBlur: React.FocusEventHandler<SVGSVGElement>;
}

export default function SideMenu({ sideNavOpen, onCloseClick, onSideNavBlur}: SideNavProps) {

	const {isDarkMode} = useTheme();

	const sideNavRef = useRef<HTMLDivElement>(null);

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
	};}, [sideNavOpen, onCloseClick]);

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
				<div className="link-container">
					<Home />
					<strong><a>Overview</a></strong>
				</div>
			</div>
		</div>
	)
}