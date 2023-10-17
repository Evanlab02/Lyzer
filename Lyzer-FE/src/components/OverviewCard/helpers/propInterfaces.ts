export interface OverviewCardProps {
    title: string;
    values: OverviewSectionValue[];
}

interface OverviewSectionValue {
    label: string;
    value: string;
}