export default function Leaderboard({ leaderboard, requireLeaderboardUpdate }) {
	return (
		<div className="leaderboard planet-panel">
			<h2>Leaderboard</h2>
			<table>
				<tbody>
					{leaderboard.map((line, index) =>
						<tr key={index}>
							<td>{index + 1}</td>
							<td>{line.name}</td>
							<td>{line.points}</td>
						</tr>
					)}
				</tbody>
			</table>
		</div>
	);
}