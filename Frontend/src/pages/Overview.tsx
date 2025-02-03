import useOverview from "../hooks/useOverview";

export default function Overview() {
	const {overviewData} = useOverview();

	return (
		<div>
            Hello, {overviewData?.fact}
		</div>
	);
};