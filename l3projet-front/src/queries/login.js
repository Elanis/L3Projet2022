import { SERVER_URL } from '../config';

import { error } from '../helpers/audio';

export default async function login(user, password, setToken) {
	if(user.trim() === '' || password.trim() === '') {
		error();
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
			error();
			return 'Incorrect username and/or password';
		}

		return res.statusText;
	}

	setToken(await res.text());

	return null;
}
