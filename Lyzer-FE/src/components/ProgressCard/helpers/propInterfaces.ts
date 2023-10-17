export interface ProgressCardProps {
    title: string;
    subtitle: string;
    progressValue: number;
    variant?: "in-progress" | "done" | "almost" | "red-bull";
}