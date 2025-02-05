import { PropsWithChildren } from "react";

export default function CardHeading(props: PropsWithChildren) {
    const { children } = props;

    return (
        <span className="lyzer-card-heading">
            {children}
        </span>
    )
}