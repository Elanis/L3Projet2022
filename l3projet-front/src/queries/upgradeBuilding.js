import fetchWithAuth from '../helpers/fetchWithAuth';

export default async function upgradeBuilding(token, enqueueSnackbar, requestPlanetListUpdate, planetId, buildingId) {
	const res = await fetchWithAuth('/planets/upgrade', token, {
		method: 'post',
		headers: {'Content-Type': 'application/json'},
		body: JSON.stringify({
			id: planetId,
			type: buildingId
		})
	});
	if(res.status !== 200) {
		enqueueSnackbar('Error while upgrading building.', { variant: 'error' });
	}
	requestPlanetListUpdate();
}