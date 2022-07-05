import { SERVER_URL } from '../config';

export default async function fetchWithAuth(url, token, options = {}) {
	return await fetch(SERVER_URL + url, {
		...options,
		headers: new Headers({
			...(options.headers || {}),
			'Authorization': `Bearer ${token}`, 
		}), 
	});
}