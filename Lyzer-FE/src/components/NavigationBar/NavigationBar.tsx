import { Link } from "react-router-dom";
import SlIcon from "@shoelace-style/shoelace/dist/react/icon/index.js";
import SlIconButton from "@shoelace-style/shoelace/dist/react/icon-button/index.js";
import "./styles/NavigationBar.scss";

export default function NavBar() {
    return (
        <header className="navbar">
            <div className="main-item navbar-item">
                <Link to={""}><SlIcon id="lyzer-logo" name="activity" />Lyzer</Link>
                <div className="menu-list-button">
                    <SlIconButton name="list" />
                </div>
            </div>
        </header>
    );
}