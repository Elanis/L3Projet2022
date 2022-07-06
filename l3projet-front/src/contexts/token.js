import { createContext, useContext, useState } from 'react';

const TokenContext = createContext();

export const TokenProvider = ({ children }) => {
	const [token, setToken] = useState(localStorage.token || '');
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