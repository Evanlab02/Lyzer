import ReactApexChart from "react-apexcharts";
import { ApexOptions } from "apexcharts";
import "./styles/LineChart.scss";

export default function LineChart() {
    const series = [
        {
            name: "Max Verstappen",
            data: [25, 45, 65, 70, 80, 90],
        },
        {
            name: "Lewis Hamilton",
            data: [15, 25, 50, 70, 75, 75],
        }
    ];

    const options = {
        colors: ['#3b5fe2', '#00D2FF'],
        chart: {
            foreColor: "#fff",
            height: 350,
            type: 'line',
            zoom: {
                enabled: false
            }
        },
        dataLabels: {
            enabled: true
        },
        stroke: {
            curve: 'straight'
        },
        grid: {
            row: {
                colors: ['transparent'],
                opacity: 0.5
            },
        },
        xaxis: {
            categories: ['Bahrain', 'Baku', 'Hungary', 'Spa', 'Zandvoort', 'Qatar'],
        },
        legend: {
            show: true,
        }
    } as ApexOptions;

    return (
        <div className="line-chart-wrapper">
            <p className="line-chart-title">
                Driver Championship Results (Last 5 races - Top 5 Drivers)
            </p>
            <div className="line-chart">
                <ReactApexChart options={options} series={series} type="line" height={350} />
            </div>
        </div>
    );
}