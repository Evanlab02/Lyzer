import { OverviewCardProps } from "./helpers/propInterfaces";
import "./styles/OverviewCard.scss";

export default function OverviewCard(props: OverviewCardProps) {
    const { title, values } = props;

    return (
        <div className="overview-card">
            <p className="card-heading">{title}</p>
            <div className="card-body">
                {values.map((value) => {
                    return (
                        <>
                            <div className="card-section">
                                <span className="section-value">{value.value}</span>
                                <span className="section-heading">{value.label}</span>
                            </div>
                        </>
                    );
                })}
            </div>
        </div >
    );
}