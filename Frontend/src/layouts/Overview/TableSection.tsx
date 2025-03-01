import Card from "../../components/Card";
import { GridItem } from "../../components/Grid";

export default function TableSection() {
	return (
		<>
			<GridItem xs={12} sm={12} md={6} lg={4} xl={3} xxl={3}>
				<Card>
					<h1>Overview Section</h1>
				</Card>
			</GridItem>
			<GridItem xs={12} sm={12} md={6} lg={4} xl={4} xxl={4}>
				<Card>
					<h1>Constructor Standings</h1>
				</Card>
			</GridItem>
			<GridItem xs={12} sm={12} md={6} lg={4} xl={5} xxl={5}>
				<Card>
					<h1>Driver Standings</h1>
				</Card>
			</GridItem>
		</>
	);
}