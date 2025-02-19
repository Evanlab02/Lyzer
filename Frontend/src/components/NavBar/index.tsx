import { Menu, Moon, Sun } from "lucide-react";
import { useTheme } from "../../hooks/useTheme";
import "./styles/index.scss";

interface NavBarProps {
	testId?: string;
	menuToggleTestId?: string;
	themeToggleTestId?: string;
	onMenuClick: (event: React.MouseEvent<SVGElement>) => void;
}

export default function NavBar(props: Readonly<NavBarProps>) {
	const {
		testId,
		menuToggleTestId,
		themeToggleTestId,
		onMenuClick
	} = props;
	
	const {
		isDarkMode, 
		toggleTheme
	} = useTheme();

	return (
		<div className="navbar" data-testid={testId}>
			<Menu 
				color={isDarkMode ? "white" : "black"} 
				size={28} 
				onClick={onMenuClick} 
				className="icon menu"
				id="navbar-menu"
				data-testid={menuToggleTestId}
			/>
			<div>L Y Z E R</div>
			{
				isDarkMode ? 
					<Moon
						id="theme-toggle-dark"
						className="icon theme-toggle"
						color="white"
						size={28}
						data-testid={themeToggleTestId}
						onClick={() => { toggleTheme(); }}
					/>
					:
					<Sun
						id="theme-toggle-light"
						className="icon theme-toggle"
						color="black"
						size={28}
						data-testid={themeToggleTestId}
						onClick={() => { toggleTheme(); }}
					/>
				
			}
		</div>
	);
}