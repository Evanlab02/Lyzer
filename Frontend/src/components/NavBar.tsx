import { useTheme } from "../hooks/useTheme";

type NavBarProps = {
	onMenuClick: (event: React.MouseEvent<HTMLDivElement>) => void;
}

export default function NavBar({ onMenuClick}: NavBarProps) {
	const {isDarkMode, toggleTheme} = useTheme();

	return (
		<div className="navbar">
			<div onClick={onMenuClick} id="menu-button" className="icon menu">
				<img src={isDarkMode ? "/menu_white.png" : "/menu_black.png"} className="icon"></img>
			</div>
			<div>
				​🇱​​🇾​​🇿​​🇪​​🇷​
			</div>
			<div>
				<img className="icon theme-toggle"
					src={isDarkMode ? "/dark_mode.png" : "/light_mode.png"}
					id="theme-toggle"
					onClick={() => {toggleTheme()}}
				></img>
			</div>
		</div>
	);
}