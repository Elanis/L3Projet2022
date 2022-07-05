import { SERVER_URL } from '../config';

export default async function fetchWithAuth(url, token, options = {}) {
	return await fetch(SERVER_URL + url, { 
		headers: new Headers({
			'Authorization': `Bearer ${token}`, 
			...(options.headers || {})
		}), 
		...options
	});
}