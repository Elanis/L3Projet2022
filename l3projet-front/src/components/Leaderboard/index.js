import LeaderboardView from './view';

import useLeaderboard from '../../contexts/leaderboard';

import './index.css';

export default function Leaderboard(props) {
	const { leaderboard, requireLeaderboardUpdate } = useLeaderboard();

	return (
		<LeaderboardView
			leaderboard={leaderboard}
			requireLeaderboardUpdate={requireLeaderboardUpdate}
		/>
	)
}