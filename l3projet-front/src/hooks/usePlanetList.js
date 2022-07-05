import { useEffect, useState } from 'react';

import { useSnackbar } from 'notistack';

import useToken from '../contexts/token';
import fetchWithAuth from '../helpers/fetchWithAuth';

export default function usePlanetList(shouldUpdate) {
	const { token } = useToken();
	const [planetsList, setPlanetsList] = useState([]);
	const { enqueueSnackbar } = useSnackbar(); 

	console.log(shouldUpdate);

	useEffect(() => {
		async function getPlanetsList() {
			const res = await fetchWithAuth('/planets/mine', token);
			if(res.status !== 200) {
				enqueueSnackbar('Error while fetching planets list.', { variant: 'error' });
				return;
			}

			setPlanetsList(await res.json());
		}
		getPlanetsList();
	}, [token, shouldUpdate]);

	return planetsList;
}