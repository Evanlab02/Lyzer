import CardBody from "./CardBody";
import CardHeading from "./CardHeading";
import { PropsWithChildren } from "react";
import "./styles/index.scss";

export default function Card(props: PropsWithChildren) {
    const { children } = props;

    return(
        <div className="lyzer-card">
            {children}
        </div>
    );
}

export { CardBody, CardHeading };
