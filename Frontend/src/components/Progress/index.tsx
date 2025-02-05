import { useMemo, useState, useEffect, useRef } from "react";
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
        useProgressColour = false
    } = props;

    const [animatedValue, setAnimatedValue] = useState(0);
    const previousValue = useRef(0);

    const progressClassName = useMemo(() => {
        const progressPercentage = (value / max) * 100;
        if (useProgressColour && progressPercentage == 100) {
            return "lyzer-progress-complete";
        } else if (useProgressColour && 80 <= progressPercentage && progressPercentage < 100) {
            return "lyzer-progress-almost";
        } else if (useProgressColour && progressPercentage < 80) {
            return "lyzer-progress-incomplete";
        }
    }, [value, max, useProgressColour]);

    // AI Generated Animation :)
    useEffect(() => {
        const startValue = previousValue.current;
        const endValue = value;
        const startTime = Date.now();

        const animateProgress = () => {
            const currentTime = Date.now();
            const elapsed = currentTime - startTime;
            const progress = Math.min(elapsed / 1000, 1);

            // Easing function for smooth animation
            const easeOutCubic = (x: number) => 1 - Math.pow(1 - x, 3);
            const easedProgress = easeOutCubic(progress);

            // Calculate the current value
            const currentValue = startValue + (endValue - startValue) * easedProgress;
            setAnimatedValue(currentValue);

            if (progress < 1) {
                requestAnimationFrame(animateProgress);
            }
        };

        requestAnimationFrame(animateProgress);
        previousValue.current = value;
    }, [value]);
    // End of AI Generated Animation :)

    return (
        <progress
            className={`lyzer-progress ${progressClassName}`}
            value={animatedValue}
            max={max}
        />
    );
}