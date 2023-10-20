import ReactApexChart from "react-apexcharts";
import { ApexOptions } from "apexcharts";
import "./styles/BarChart.scss";

export default function BarChart() {
    const series = [
        {
            name: 'Mercedes',
            data: [30]
        },
        {
            name: 'Ferrari',
            data: [20]
        },
        {
            name: 'Red Bull',
            data: [50],
        },
        {
            name: 'McLaren',
            data: [10]
        },
        {
            name: 'Alpine',
            data: [10]
        }
    ];

    const options = {
        colors: ['#00D2FF', '#FF0000', '#3b5fe2', '#FF8700', '#0090FF'],
        chart: {
            foreColor: window.matchMedia("(prefers-color-scheme: dark)").matches ? "#fff" : "#000",
            type: 'bar',
            height: 350
        },
        plotOptions: {
            bar: {
                horizontal: false,
                columnWidth: '55%',
                endingShape: 'rounded'
            },
        },
        dataLabels: {
            enabled: false
        },
        stroke: {
            show: true,
            width: 2,
            colors: ['transparent']
        },
        xaxis: {
            categories: ['Qatar'],
        },
        yaxis: {
            title: {
                text: 'Points'
            }
        },
        fill: {
            opacity: 1
        },
        tooltip: {
            y: {
                formatter: function (val: number) {
                    return val + " Points";
                }
            }
        }
    } as ApexOptions;

    return (
        <div className="bar-chart-wrapper" >
            <p className="bar-chart-title">
                Constructor Points (Last Race)
            </p>
            <div className="bar-chart">
                <ReactApexChart options={options} series={series} type="bar" height={350} />
            </div>
        </div >
    );
}