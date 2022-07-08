import { useSnackbar } from 'notistack';

import usePlanetList from '../../contexts/planetList';
import useRedirectIfNotAuthenticated from '../../hooks/useRedirectIfNotAuthenticated';

import Leaderboard from '../Leaderboard';
import PlanetView from '../PlanetView';

import './index.css';

export default function Game() {
	useRedirectIfNotAuthenticated();

	const { planetList } = usePlanetList();

	// TODO: change planet

	if(planetList.length === 0) {
		return <img className="loading-gif" src="img/loading.png" alt="loading" />;
	}

	return (
		<>
			<PlanetView planet={planetList[0]} />
			<Leaderboard />
		</>
	);
}