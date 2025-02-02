import { GridContainer, GridItem } from "../components/Grid";

export default function Overview() {
	return (
		<div data-testid="lyzer-overview-page">
            <GridContainer>
				<GridItem xs={12} sm={12} md={6} lg={6} xl={4} xxl={4}>
					<div>IDK</div>
				</GridItem>
				<GridItem xs={12} sm={12} md={6} lg={6} xl={4} xxl={4}>
					<div>IDK</div>
				</GridItem>
			</GridContainer>
		</div>
	);
};