import NavBar from "./components/NavBar";
import Navigation from "./routes/Navigation";
import "./styles/index.scss";

function App() {
	function onMenuClickCallback() {
		console.log("Menu clicked");
	}

	return (
		<>
			<NavBar onMenuClick={onMenuClickCallback}/>
			<Navigation />
		</>
	);
}

export default App;