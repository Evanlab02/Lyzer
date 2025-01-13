import { Menu, Moon, Sun } from "lucide-react";
import { useTheme } from "../hooks/useTheme";
import "../styles/navbar.scss";

type NavBarProps = {
	onMenuClick: (event: React.MouseEvent<SVGElement>) => void;
}

export default function NavBar({ onMenuClick}: NavBarProps) {
	const {isDarkMode, toggleTheme} = useTheme();

	return (
		<div className="navbar">
			<Menu 
				color={isDarkMode ? "white" : "black"} 
				size={28} onClick={onMenuClick} 
				className="icon menu"
				id="navbar-menu"
			/>
			<div>
				​🇱​​🇾​​🇿​​🇪​​🇷​
			</div>
			{
				isDarkMode ? (
					<Moon
						id="theme-toggle-dark"
						className="icon theme-toggle"
						color="white"
						size={28}
						onClick={() => toggleTheme()}
					/>
				) : (
					<Sun
						id="theme-toggle-light"
						className="icon theme-toggle"
						color="black"
						size={28}
						onClick={() => toggleTheme()}
					/>
				)
			}
		</div>
	);
}