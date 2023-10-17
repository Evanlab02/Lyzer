import "./styles/PanelTable.scss";

export default function PanelTable() {
    return (
        <div className="table-panel">
            <p className="table-panel-title">Standings</p>
            <div className="table-data">
                <table>
                    <thead>
                        <tr>
                            <th>Position</th>
                            <th>Driver</th>
                            <th>Team</th>
                            <th>Points</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>1</td>
                            <td>Max Verstappen</td>
                            <td>Red Bull Racing Honda</td>
                            <td>400</td>
                        </tr>
                        <tr>
                            <td>2</td>
                            <td>Lewis Hamilton</td>
                            <td>Mercedes</td>
                            <td>369</td>
                        </tr>
                        <tr>
                            <td>3</td>
                            <td>Valtteri Bottas</td>
                            <td>Mercedes</td>
                            <td>226</td>
                        </tr>
                        <tr>
                            <td>4</td>
                            <td>Lando Norris</td>
                            <td>McLaren Mercedes</td>
                            <td>224</td>
                        </tr>
                        <tr>
                            <td>5</td>
                            <td>Sergio Perez</td>
                            <td>Red Bull Racing Honda</td>
                            <td>190</td>
                        </tr>
                        <tr>
                            <td>6</td>
                            <td>Charles Leclerc</td>
                            <td>Ferrari</td>
                            <td>158</td>
                        </tr>
                        <tr>
                            <td>7</td>
                            <td>Carlos Sainz</td>
                            <td>Ferrari</td>
                            <td>150</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    );
}