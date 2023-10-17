import { Link } from "react-router-dom";
import SlIcon from "@shoelace-style/shoelace/dist/react/icon/index.js";
import SlIconButton from "@shoelace-style/shoelace/dist/react/icon-button/index.js";
import { NavBarProps } from "./helpers/propInterfaces";

import "./styles/NavigationBar.scss";

export default function NavBar(props: NavBarProps) {
    const { onMenuClick } = props;

    return (
        <header className="navbar">
            <div className="main-item navbar-item">
                <Link to={""}>
                    <SlIcon id="lyzer-logo" name="activity" />
                    <div id="lyzer-logo-text">Lyzer</div>
                </Link>
                <SlIconButton className="navbar-button" name="list" onClick={onMenuClick} />
            </div>
        </header>
    );
}