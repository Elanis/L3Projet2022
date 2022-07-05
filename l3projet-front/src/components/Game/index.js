import { useSnackbar } from 'notistack';

import usePlanetList from '../../contexts/planetList';
import useRedirectIfNotAuthenticated from '../../hooks/useRedirectIfNotAuthenticated';

import PlanetView from '../PlanetView';

export default function Game() {
	useRedirectIfNotAuthenticated();

	const { planetList } = usePlanetList();

	console.log(planetList);

	// TODO: change planet

	if(planetList.length === 0) {
		// TODO: Loading
		return null;
	}

	return (
		<PlanetView planet={planetList[0]} />
	);
}