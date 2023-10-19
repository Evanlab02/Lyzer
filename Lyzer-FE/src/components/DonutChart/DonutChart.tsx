import ReactApexChart from "react-apexcharts";
import { ApexOptions } from "apexcharts";
import "./styles/DonutChart.scss";

export default function DonutChart() {
    const series = [44, 55, 41, 17, 15, 10];
    const options = {
        colors: ['#00D2FF', '#FF0000', '#3b5fe2', '#FF8700', '#0090FF', '#9b00d9'],
        chart: {
            foreColor: window.matchMedia("(prefers-color-scheme: dark)").matches ? "#fff" : "#000",
            type: 'donut',
            width: 380,
        },
        stroke: {
            show: false,
        },
        labels: ["Mercedes", "Ferrari", "Red Bull", "Mclaren", "Alpine", "Others"],
        responsive: [{
            breakpoint: 480,
            options: {
                chart: {
                    width: 200
                },
                legend: {
                    position: 'bottom'
                }
            }
        }]
    } as ApexOptions;

    return (
        <div className="donut-chart-wrapper" >
            <p className="donut-chart-title">
                Point Shares
            </p>
            <div className="donut-chart">
                <ReactApexChart options={options} series={series} type="donut" height={350} />
            </div>
        </div >
    );
}