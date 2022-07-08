import { SERVER_URL } from '../config';

import { error } from '../helpers/audio';

export default async function register({
		username,
		password,
		passwordConfirmation,
		mail
	}, setToken) {

	username = username.trim();
	password = password.trim();
	passwordConfirmation = passwordConfirmation.trim();
	mail = mail.trim();

	if(username === '') {
		error();
		return 'Error: Empty username';
	}
	if(password === '') {
		error();
		return 'Error: Empty password';
	}
	if(mail === '') {
		error();
		return 'Error: Empty mail';
	}

	if(username.length < 4) {
		error();
		return 'Error: Username too short !';
	}

	if(password !== passwordConfirmation) {
		error();
		return 'Error: Password mismatch';
	}

	const passwordRegexp = new RegExp("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%_^&*-]).{8,}$");
	if (!passwordRegexp.test(password)) {
		error();
		return 'Error: Invalid password content !';
	}

	const mailRegexp = new RegExp("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e \\-\\x1f\\x21\\x23 -\\x5b\\x5d -\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e -\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])");
	if (!mailRegexp.test(mail)) {
		error();
		return 'Error: Invalid mail !';
	}

	const options = {
		method: 'POST',
		headers: {'Content-Type': 'application/json'},
		body: `{"username":"${username}","password":"${password}","confirmationPassword":"${passwordConfirmation}","mail":"${mail}"}`
	};

	const res = await fetch(`${SERVER_URL}/register`, options);
	if(res.status !== 200) {
		error();
		if(res.status === 401) {
			return 'Incorrect username and/or password'
		}

		return res.statusText;
	}

	setToken(await res.text());

	return null;
}
