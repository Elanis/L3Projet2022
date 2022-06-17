import useToken from '../../contexts/token';

export default function Game() {
	const { token } = useToken();
	console.log(token);

	return <div>Game !</div>
}