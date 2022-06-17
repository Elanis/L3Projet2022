import { SERVER_URL } from '../config';

export default async function login(user, password) {
	if(user.trim() === '' || password.trim() === '') {
		return 'Error: Empty username and/or password';
	}

	const options = {
		method: 'POST',
		headers: {'Content-Type': 'application/json'},
		body: `{"username":"${user}","password":"${password}"}`
	};

	const res = await fetch(`${SERVER_URL}/auth`, options);
	if(res.status !== 200) {
		if(res.status === 401) {
			return 'Incorrect username and/or password'
		}

		return res.statusText;
	}
	return null;
}
