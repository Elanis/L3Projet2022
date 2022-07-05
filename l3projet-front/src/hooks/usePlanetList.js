import { useEffect, useState } from 'react';

import useToken from '../contexts/token';
import fetchWithAuth from '../helpers/fetchWithAuth';

export default function usePlanetList(enqueueSnackbar) {
	const { token } = useToken();
	const [planetsList, setPlanetsList] = useState([]);

	useEffect(() => {
		async function getPlanetsList() {
			const res = await fetchWithAuth('/planets/mine', token);
			if(res.status !== 200) {
				enqueueSnackbar('Error while fetching planets list.', { variant: 'error' });
			}

			setPlanetsList(await res.json());
		}
		getPlanetsList();
	}, []);

	return planetsList;
}