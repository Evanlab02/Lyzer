import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import SlIcon from "@shoelace-style/shoelace/dist/react/icon/index.js";
import SlIconButton from "@shoelace-style/shoelace/dist/react/icon-button/index.js";
import { SideBarProps } from "./helpers/propInterfaces";
import "./styles/SideBar.scss";

export default function SideBar(props: SideBarProps) {
    const { isMenuOpen, onMenuClose } = props;

    const [hiddenClass, setHiddenClass] = useState("sidebar-hidden");

    useEffect(() => {
        if (isMenuOpen)
            setHiddenClass("");
        else
            setHiddenClass("sidebar-hidden");
    }, [isMenuOpen]);

    return (
        <div id="app-sidebar" className={`sidebar ${hiddenClass}`}>
            <div className="sidebar-item">
                <Link to={""}>
                    <SlIcon id="lyzer-logo" name="activity" />
                    <div id="lyzer-logo-text">Lyzer</div>
                </Link>
                <SlIconButton className="sidebar-button" name="x" onClick={onMenuClose} />
            </div>
            <div className="sidebar-item">
                <Link to={""}>
                    <SlIcon className="sidebar-logo" name="house-fill" />
                    <div>Overview</div>
                </Link>
            </div>
        </div>
    );
}