import { memo } from "react";
import SkeletonLoader from "../SkeletonLoader";
import "./styles/index.scss";

export interface TableCell {
	value: string | number;
	altValue?: string | number;
	width?: string;
	fontWeight?: string;
	customClass?: string;
	vanish?: string;
}

export interface TableColumn {
	display: string;
	vanish?: boolean;
	width?: string;
	minWidth?: string;
	maxWidth?: string;
}

export interface TableRow {
	values: TableCell[];
	color?: string;
}

interface TableHeadProps {
	columns: TableColumn[];
}

interface TableBodyProps {
	rows: TableRow[];
}

export interface TableProps {
	columns: TableColumn[];
	rows: TableRow[];
	loading?: boolean;
	loadingRows?: number;
}

const TableHead = memo(function TableHead(props: TableHeadProps) {
	const { columns } = props;

	return (
		<thead>
			<tr>
				{columns.map((column, index) => {
					const alignClass = index !== columns.length - 1 ? "text-left" : "text-right";
					const vanishClass = column.vanish ? "th-vanish" : "";
					return (
						<th
							key={index}
							className={`${alignClass} ${vanishClass}`}
							style={{
								width: column.width,
								minWidth: column.minWidth,
								maxWidth: column.maxWidth,
							}}
						>
							{column.display}
						</th>
					);
				})}
			</tr>
		</thead>
	);
});

const TableBody = memo(function TableBody(props: TableBodyProps) {
	const { rows } = props;

	return (
		<tbody>
			{rows.map((row, rowIndex) => (
				<tr key={rowIndex}>
					{row.values.map((value, index) => {
						const alignClass = index !== rows[0].values.length - 1 ? "text-left" : "text-right";
						const hasBorder = index === 0 && row.color;
						const className = hasBorder ? `${alignClass} with-border` : alignClass;
						const vanishClass = value.vanish ? "td-vanish" : "";

						return (
							<td
								key={index}
								className={`${className} ${vanishClass} ${value.customClass}`}
								style={{
									color: hasBorder ? row.color : undefined,
									fontWeight: value.fontWeight,
									width: value.width,
								}}
							>
								<span className="table-default-value">{value.value}</span>
								<span className="table-alt-value">{value.altValue ?? value.value}</span>
							</td>
						);
					})}
				</tr>
			))}
		</tbody>
	);
});

interface TableBodyLoadingProps {
	columns: TableColumn[];
	loadingRows: number;
}

const TableBodyLoading = memo(function TableBodyLoading(props: TableBodyLoadingProps) {
	const { columns, loadingRows } = props;

	return (
		<tbody>
			{Array.from({ length: loadingRows }).map((_, rowIndex) => (
				<tr key={rowIndex}>
					{columns.map((column, index) => {
						const alignClass = index !== columns.length - 1 ? "text-left" : "text-right";
						const vanishClass = column.vanish ? "td-vanish" : "";

						return (
							<td
								key={index}
								className={`${alignClass} ${vanishClass}`}
								style={{
									width: column.width,
									minWidth: column.minWidth,
									maxWidth: column.maxWidth,
								}}
							>
								<SkeletonLoader height="20px" />
							</td>
						);
					})}
				</tr>
			))}
		</tbody>
	);
});

export default function Table(props: TableProps) {
	const { columns, rows, loading = false, loadingRows = 5 } = props;

	return (
		<div className="lyzer-table-wrapper">
			<table className="lyzer-table">
				<TableHead columns={columns} />
				{loading ? (
					<TableBodyLoading columns={columns} loadingRows={loadingRows} />
				) : (
					<TableBody rows={rows} />
				)}
			</table>
		</div>
	);
}
