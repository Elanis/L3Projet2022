import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useSnackbar } from 'notistack';
import useToken from '../../contexts/token';

import register from '../../queries/register';

export default function RegisterForm({ switchForm }) {
	const [username, setUsername] = useState('');
	const [password, setPassword] = useState('');
	const [passwordConfirmation, setPasswordConfirmation] = useState('');
	const [mail, setMail] = useState('');
	const { setToken } = useToken();

	const navigate = useNavigate();
	const { enqueueSnackbar } = useSnackbar();

	const onSubmit = async() => {
		const err = await register({
			username,
			password,
			passwordConfirmation,
			mail
		}, setToken);
		if(err !== null) {
			enqueueSnackbar(err || 'Unknown error', { variant: 'error' });
			return;
		}

		navigate('/game', { replace: true });
	}

	return (
		<div className="homepage-form">
			<h2 className="homepage-title">Register</h2>
			<input type="text" placeholder="Username" onChange={(e) => setUsername(e.target.value)} required />
			<input type="password" placeholder="Password" onChange={(e) => setPassword(e.target.value)} required />
			<input type="password" placeholder="Password Confirmation" onChange={(e) => setPasswordConfirmation(e.target.value)} required />
			<input type="text" placeholder="Mail" onChange={(e) => setMail(e.target.value)} required />
			<input type="button" value="Register" onClick={onSubmit} />
			<br/>
			<span className="homepage-link" onClick={switchForm}>Already have an account ?</span>
		</div>
	);
}