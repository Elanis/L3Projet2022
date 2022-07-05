import { useEffect } from 'react';

import { useNavigate } from 'react-router-dom';
import useToken from '../contexts/token';

export default function useRedirectIfNotAuthenticated() {
	const navigate = useNavigate();
	const { isAuthenticated } = useToken();

	useEffect(() => {
		if(!isAuthenticated) {
			navigate('/', { replace: true });
		}
	}, []);
}