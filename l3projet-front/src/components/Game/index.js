import { useSnackbar } from 'notistack';

import usePlanetList from '../../hooks/usePlanetList';
import useRedirectIfNotAuthenticated from '../../hooks/useRedirectIfNotAuthenticated';

export default function Game() {
	useRedirectIfNotAuthenticated();

	const { enqueueSnackbar } = useSnackbar();
	const planets = usePlanetList(enqueueSnackbar);

	console.log(planets);

	return <div>Game !</div>
}