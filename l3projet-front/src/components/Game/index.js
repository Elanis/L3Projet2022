import { useSnackbar } from 'notistack';

import usePlanetList from '../../hooks/usePlanetList';
import useRedirectIfNotAuthenticated from '../../hooks/useRedirectIfNotAuthenticated';

import PlanetView from '../PlanetView';

export default function Game() {
	useRedirectIfNotAuthenticated();

	const { enqueueSnackbar } = useSnackbar();
	const planets = usePlanetList(enqueueSnackbar);

	console.log(planets);

	// TODO: change planet

	if(planets.length === 0) {
		// TODO: Loading
		return null;
	}

	return (
		<PlanetView planet={planets[0]} />
	);
}