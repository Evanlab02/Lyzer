import { Grid } from "@mui/material";
import NavBar from "../components/NavigationBar/NavigationBar";

export default function Overview() {
    return (
        <>
            <Grid container spacing={0}>
                <Grid item xs={12}>
                    <NavBar />
                </Grid>
            </Grid>
            <h1>Overview</h1>
        </>
    )
}