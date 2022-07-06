import { createContext, useContext, useState } from 'react';

import useLeaderboard from '../hooks/useLeaderboard';

const LeaderboardContext = createContext();

export const LeaderboardProvider = ({ children }) => {
	const [shouldUpdate, setShouldUpdate] = useState(0);
	const leaderboard = useLeaderboard(shouldUpdate);

	return (
		<LeaderboardContext.Provider value={{ leaderboard, requireLeaderboardUpdate: () => setShouldUpdate(Date.now()) }}>
			{children}
		</LeaderboardContext.Provider>
	);
};

export default function useLeaderboardFromContext() {
	const { leaderboard, requireLeaderboardUpdate } = useContext(LeaderboardContext);

	return { leaderboard, requireLeaderboardUpdate };
}