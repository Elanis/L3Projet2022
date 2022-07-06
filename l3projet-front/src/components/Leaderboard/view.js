export default function Leaderboard({ leaderboard, requireLeaderboardUpdate }) {
	console.log({ leaderboard, requireLeaderboardUpdate });

	return (
		<div className="leaderboard planet-panel">
			<h2>Leaderboard</h2>
			<table>
				{leaderboard.map((line, index) =>
					<tr>
						<td>{index + 1}</td>
						<td>{line.name}</td>
						<td>{line.points}</td>
					</tr>
				)}
			</table>
		</div>
	);
}