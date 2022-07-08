import fetchWithAuth from '../helpers/fetchWithAuth';

import { error, select } from '../helpers/audio';

export default async function upgradeBuilding(token, enqueueSnackbar, requestPlanetListUpdate, planetId, buildingId) {
	try {
		const res = await fetchWithAuth('/planets/upgrade', token, {
			method: 'post',
			headers: {'Content-Type': 'application/json'},
			body: JSON.stringify({
				id: planetId,
				type: buildingId
			})
		});
		if(res.status !== 200) {
			error();
			enqueueSnackbar('Error while upgrading building.', { variant: 'error' });
		} else {
			select();
		}
	} catch(e) {
		error();
		enqueueSnackbar('Error while upgrading building.', { variant: 'error' });
		console.error(e);
	}
	requestPlanetListUpdate();
}