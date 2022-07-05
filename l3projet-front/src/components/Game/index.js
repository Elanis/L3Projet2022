import useToken from '../../contexts/token';
import useRedirectIfNotAuthenticated from '../../hooks/useRedirectIfNotAuthenticated';

export default function Game() {
	useRedirectIfNotAuthenticated();
	

	return <div>Game !</div>
}