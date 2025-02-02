import { PropsWithChildren } from "react";

export interface FlexGridItemProps extends PropsWithChildren {
    xs?: number
    sm?: number
    md?: number
    lg?: number
    xl?: number
    xxl?: number
}

export default function GridItem(props: Readonly<FlexGridItemProps>) {
	const { children, xs, sm, md, lg, xl, xxl } = props;

	const classes = [
		xs && `flex-grid-col-xs-${xs.toString()}`,
		sm && `flex-grid-col-sm-${sm.toString()}`,
		md && `flex-grid-col-md-${md.toString()}`,
		lg && `flex-grid-col-lg-${lg.toString()}`,
		xl && `flex-grid-col-xl-${xl.toString()}`,
		xxl && `flex-grid-col-xxl-${xxl.toString()}`
	].filter(Boolean).join(" ");

	return (
		<div className={classes}>
			{children}
		</div>
	);
}
