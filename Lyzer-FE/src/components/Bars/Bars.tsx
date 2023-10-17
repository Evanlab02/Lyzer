import { useState } from "react";
import { Grid } from "@mui/material";
import SideBar from "./SideBar/SideBar";
import NavBar from "./NavigationBar/NavigationBar";

export default function Bars() {
    const [openMenu, setOpenMenu] = useState(false);

    const onMenuClick = () => {
        setOpenMenu(!openMenu);
    }


    return (
        <>
            <SideBar isMenuOpen={openMenu} onMenuClose={onMenuClick} />
            <Grid container spacing={0}>
                <Grid item xs={12}>
                    <NavBar onMenuClick={onMenuClick} />
                </Grid>
            </Grid>
        </>
    )
}