import { useEffect, useState } from 'react';

import { useSnackbar } from 'notistack';

import useToken from '../contexts/token';
import fetchWithAuth from '../helpers/fetchWithAuth';

export default function useLeaderboard(shouldUpdate) {
	const { token } = useToken();
	const [leaderboard, setLeaderoard] = useState([]);
	const { enqueueSnackbar } = useSnackbar(); 

	useEffect(() => {
		async function getPlanetsList() {
			const res = await fetchWithAuth('/user/leaderboard', token);
			if(res.status !== 200) {
				enqueueSnackbar('Error while fetching leaderboard.', { variant: 'error' });
				return;
			}

			setLeaderoard(await res.json());
		}
		getPlanetsList();
	}, [token, shouldUpdate]);

	return leaderboard;
}