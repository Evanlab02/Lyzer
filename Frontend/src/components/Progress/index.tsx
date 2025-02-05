import { useMemo } from "react";
import "./styles/index.scss";

export interface ProgressProps {
    value?: number;
    max?: number;
    useProgressColour?: boolean;
}

export default function Progress(props: ProgressProps) {
    const { 
        value = 0, 
        max = 100,
        useProgressColour = false,
    } = props;

    const progressClassName = useMemo(() => {
        const progressPercentage = (value / max) * 100;
        console.log(progressPercentage);
        if (useProgressColour && progressPercentage == 100) {
            return "lyzer-progress-complete";
        } else if (useProgressColour && 80 <= progressPercentage && progressPercentage < 100) {
            return "lyzer-progress-almost";
        } else if (useProgressColour && progressPercentage < 80) {
            return "lyzer-progress-incomplete";
        }
    }, [value, max, useProgressColour]);

    return (
        <progress
            className={`lyzer-progress ${progressClassName}`}
            value={value}
            max={max}
        />
    );
}