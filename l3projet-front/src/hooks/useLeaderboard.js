import { useEffect, useState } from 'react';

import { useSnackbar } from 'notistack';

import useToken from '../contexts/token';
import fetchWithAuth from '../helpers/fetchWithAuth';

import { error } from '../helpers/audio';

export default function useLeaderboard(shouldUpdate) {
	const { token } = useToken();
	const [leaderboard, setLeaderoard] = useState([]);
	const { enqueueSnackbar } = useSnackbar(); 

	useEffect(() => {
		async function getLeaderboard() {
			const res = await fetchWithAuth('/user/leaderboard', token);
			if(res.status !== 200) {
				error();
				enqueueSnackbar('Error while fetching leaderboard.', { variant: 'error' });
				return;
			}

			setLeaderoard(await res.json());
		}
		getLeaderboard();
	}, [token, shouldUpdate]);

	return leaderboard;
}