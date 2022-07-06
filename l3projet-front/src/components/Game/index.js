import { useSnackbar } from 'notistack';

import usePlanetList from '../../contexts/planetList';
import useRedirectIfNotAuthenticated from '../../hooks/useRedirectIfNotAuthenticated';

import Leaderboard from '../Leaderboard';
import PlanetView from '../PlanetView';

export default function Game() {
	useRedirectIfNotAuthenticated();

	const { planetList } = usePlanetList();

	// TODO: change planet

	if(planetList.length === 0) {
		// TODO: Loading
		return null;
	}

	return (
		<>
			<PlanetView planet={planetList[0]} />
			<Leaderboard />
		</>
	);
}