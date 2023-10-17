import { lazy } from "react";
import { Grid } from "@mui/material";
import Bars from "../components/Bars/Bars";
import ProgressCard from "../components/ProgressCard/ProgressCard";
import OverviewCard from "../components/OverviewCard/OverviewCard";
import PanelTable from "../components/PanelTable/PanelTable";

const LineChart = lazy(() => import("../components/LineChart/LineChart"));
const BarChart = lazy(() => import("../components/BarChart/BarChart"));
const DonutChart = lazy(() => import("../components/DonutChart/DonutChart"));

export default function Overview() {
    return (
        <>
            <Bars />
            <div className="page-wrapper">
                <div className="page-title-container">
                    <h4 className="page-title">Overview</h4>
                </div>
                <Grid container spacing={2}>
                    <Grid item xs={12} sm={12} md={6} lg={6} xl={3}>
                        <ProgressCard
                            progressValue={85}
                            subtitle="Almost (20 Hours to go)"
                            title="Is it race weekend?"
                        />
                    </Grid>
                    <Grid item xs={12} sm={12} md={6} lg={6} xl={3}>
                        <ProgressCard
                            progressValue={0}
                            subtitle="Next session: Practice 1"
                            title="Race weekend progress"
                        />
                    </Grid>
                    <Grid item xs={12} sm={12} md={6} lg={6} xl={3}>
                        <ProgressCard
                            progressValue={100}
                            subtitle="Max Verstappen"
                            title="Driver Leader"
                            variant="red-bull"
                        />
                    </Grid>
                    <Grid item xs={12} sm={12} md={6} lg={6} xl={3}>
                        <ProgressCard
                            progressValue={100}
                            subtitle="Red Bull Racing Honda"
                            title="Constructor Leader"
                            variant="red-bull"
                        />
                    </Grid>
                    <Grid item xs={12} sm={12} md={12} lg={12} xl={3}>
                        <OverviewCard
                            title="Overview"
                            values={[
                                {
                                    label: "Last Race Winner",
                                    value: "Max Verstappen"
                                },
                                {
                                    label: "Last Track",
                                    value: "Qatar"
                                },
                                {
                                    label: "Season Progress",
                                    value: "11 of 22 races"
                                }
                            ]}
                        />
                    </Grid>
                    <Grid item xs={12} sm={12} md={12} lg={12} xl={6}>
                        <LineChart />
                    </Grid>
                    <Grid item xs={12} sm={12} md={12} lg={12} xl={3}>
                        <BarChart />
                    </Grid>
                    <Grid item xs={12} sm={12} md={12} lg={12} xl={4}>
                        <DonutChart />
                    </Grid>
                    <Grid item xs={12} sm={12} md={12} lg={12} xl={8}>
                        <PanelTable />
                    </Grid>
                </Grid>
            </div>
        </>
    )
}