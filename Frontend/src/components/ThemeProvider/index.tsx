import { useEffect, useState } from "react";
import { ThemeContext } from "../../hooks/useTheme";

export const ThemeProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
	const defaultDark = window.matchMedia("(prefers-color-scheme: dark)").matches;

	const [isDarkMode, setDarkMode] = useState(
		localStorage.getItem("isDarkMode") !== null
			? localStorage.getItem("isDarkMode") === "true"
			: defaultDark
	);

	const toggleTheme = () => {
		setDarkMode((prev) => !prev);
	};

	useEffect(() => {
		const theme = isDarkMode ? "dark" : "light";
		localStorage.setItem("isDarkMode", theme === "dark" ? "true" : "false");
		document.documentElement.setAttribute("data-theme", theme);
	}, [isDarkMode]);

	return (
		<ThemeContext.Provider value={{ isDarkMode, toggleTheme }}>
			{children}
		</ThemeContext.Provider>
	);
};