import { useEffect, useState } from "react";
import { UpcomingRaceWeekend } from "../clients/interfaces/overviewInterfaces"; 
import { getOverview } from "../clients/overviewClient"; 

export default function useRaceWeekend(season: string) {
  // State to hold the upcoming race weekend data.
  const [data, setData] = useState<UpcomingRaceWeekend | undefined>(undefined);
  // Optional state to handle error messages.
  const [error, setError] = useState<string | undefined>(undefined);
  // Optional loading state.
  const [loading, setLoading] = useState<boolean>(true);

  // Function to fetch the data from the backend.
  const fetchData = async () => {
    try {
      // Call your client function to fetch data.
      const result = await getOverview();
      setData(result);
      setError(undefined);
    } catch (err: any) {
      setError(err.message);
      setData(undefined);
    } finally {
      setLoading(false);
    }
  };

  // Call fetchData when the hook is first used and when the season changes.
  useEffect(() => {
    setLoading(true);
    void fetchData();
  }, [season]);

  // Function to manually refresh the data.
  const refreshData = () => {
    setData(undefined);
    setError(undefined);
    setLoading(true);
    void fetchData();
  };

  return { data, error, loading, refreshData };
}
