import React, { createContext, useContext, useEffect, useState } from "react";

interface ThemeContextType {
	isDarkMode: boolean;
	toggleTheme: () => void;
}

const ThemeContext = createContext<ThemeContextType | undefined>(undefined);

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

export const useTheme = (): ThemeContextType => {
	const context = useContext(ThemeContext);
	if (!context) {
		throw new Error("useTheme must be used within a ThemeProvider");
	}
	return context;
};
