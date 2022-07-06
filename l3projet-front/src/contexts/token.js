import { createContext, useContext, useEffect, useState } from 'react';

import fetchWithAuth from '../helpers/fetchWithAuth';

const TokenContext = createContext();

export const TokenProvider = ({ children }) => {
	const [token, setToken] = useState(localStorage.token || '');

	useEffect(() => {
		async function renewToken() {
			const res = fetchWithAuth('/user/renewToken', token);

			if(res.status === 200) {
				setToken(await res.text());
			}
		}
		
		const timeoutId = setTimeout(renewToken, 60*60*1000); // Renew token every hour

		return () => {
			clearTimeout(timeoutId);
		};
	}, [token]);

	return (
		<TokenContext.Provider value={{ token, setToken }}>
			{children}
		</TokenContext.Provider>
	);
};

export default function useToken() {
	const {token, setToken} = useContext(TokenContext);

	return { token, isAuthenticated: token !== '', setToken: (token) => {
		localStorage.token = token;
		return setToken(token);
	}};
}