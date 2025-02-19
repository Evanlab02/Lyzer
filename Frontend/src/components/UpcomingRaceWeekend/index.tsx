// import { useMemo } from "react";
// import "./styles/index.scss";

// export interface RaceWeekendProps {
//     isRaceWeekend?: boolean;
//     raceWeekendProgress?: number;
// 	timeToRaceWeekend?: number;
//     useProgressColour?: boolean;
//     testId?: string;
// }

// export default function RaceWeekendCard(props: RaceWeekendProps) {
// 	const { 
// 		isRaceWeekend = false;
// 		raceWeekendProgress = 0;
// 		timeToRaceWeekend = 0;
// 		useProgressColour = false;
// 		testId;
// 	} = props;

// 	const progressClassName = useMemo(() => {
// 		const progressPercentage = (value / max) * 100;
// 		if (useProgressColour && progressPercentage == 100) {
// 			return "lyzer-progress-complete";
// 		} else if (useProgressColour && 80 <= progressPercentage && progressPercentage < 100) {
// 			return "lyzer-progress-almost";
// 		} else if (useProgressColour && progressPercentage < 80) {
// 			return "lyzer-progress-incomplete";
// 		}
// 	}, [value, max, useProgressColour]);

// 	return (
// 		<progress
// 			className={`lyzer-progress ${progressClassName ?? ""}`}
// 			value={value}
// 			max={max}
// 			data-testid={testId}
// 		/>
// 	);
// }

import React, { useState, useEffect } from "react";
import Progress from "../Progress";
import { UpcomingRaceWeekend } from "../../clients/interfaces/overviewInterfaces";
import "./styles/raceWeekendCard.scss";

export default function RaceWeekendCard() {
  const [data, setData] = useState<UpcomingRaceWeekend | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    async function fetchData() {
      try {
        const response = await fetch("/api/v1/lyzer/overview");
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
        const result: UpcomingRaceWeekend = await response.json();
        setData(result);
      } catch (err: any) {
        setError(err.message);
      }
    }
    fetchData();
  }, []);

  // Helper to format minutes into days, hours, and minutes.
  function formatTime(minutes: number) {
    const days = Math.floor(minutes / (24 * 60));
    const hours = Math.floor((minutes % (24 * 60)) / 60);
    const mins = minutes % 60;
    return `${days}d ${hours}h ${mins}m`;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }
  if (!data) {
    return <div>Loading...</div>;
  }

  return (
    <div className="race-weekend-card">
      <h2>Race Weekend Status</h2>
      <p>{data.status}</p>
      {!data.isRaceWeekend && (
        <p>Time until race weekend: {formatTime(data.timeToRaceWeekend)}</p>
      )}
      <Progress 
        value={data.timeToRaceWeekendProgress}
        max={100}
        useProgressColour={true}
        testId="race-weekend-progress"
      />
    </div>
  );
}

