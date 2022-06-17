import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useSnackbar } from 'notistack';
import useToken from '../../contexts/token';

import login from '../../queries/login';

export default function LoginForm({ switchForm }) {
	const [username, setUsername] = useState('');
	const [password, setPassword] = useState('');
	const navigate = useNavigate();
	const { enqueueSnackbar } = useSnackbar();
	const { setToken } = useToken();

	const onSubmit = async() => {
		const err = await login(username, password, setToken);
		if(err !== null) {
			enqueueSnackbar(err || 'Unknown error', { variant: 'error' });
			return;
		}

		navigate('/game', { replace: true });
	}

	return (
		<div className="homepage-form">
			<h2 className="homepage-title">Login</h2>
			<input type="text" placeholder="Username" onChange={(e) => setUsername(e.target.value)} required />
			<input type="password" placeholder="Password" onChange={(e) => setPassword(e.target.value)} required />
			<input type="button" value="Login" onClick={onSubmit} />
			<br/>
			<span className="homepage-link" onClick={switchForm}>No account yet ? Register now !</span>
		</div>
	);
}