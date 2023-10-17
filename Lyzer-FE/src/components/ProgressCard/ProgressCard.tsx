import { useEffect, useState } from "react";
import SlProgressBar from "@shoelace-style/shoelace/dist/react/progress-bar/index.js";
import { ProgressCardProps } from "./helpers/propInterfaces";
import "./styles/Cards.scss";
import "./styles/ProgressCard.scss"

export default function ProgressCard(props: ProgressCardProps) {
    const { progressValue, subtitle, title, variant = undefined } = props;
    const [progressClass, setProgressClass] = useState<string>("");

    useEffect(() => {
        if (variant)
            setProgressClass(variant);
        else if (progressValue >= 100)
            setProgressClass("success");
        else if (progressValue > 80)
            setProgressClass("almost");
        else
            setProgressClass("in-progress")
    }, [variant, progressValue]);

    return (
        <div className="progress-card">
            <div className="card-body">
                <span className="card-heading">{title}</span>
                <h3 className="card-value">{subtitle}</h3>
                <SlProgressBar className={progressClass} value={progressValue} />
            </div>
        </div>
    );
}