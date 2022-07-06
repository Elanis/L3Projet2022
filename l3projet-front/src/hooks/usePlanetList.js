import { useEffect, useState } from 'react';

import { useSnackbar } from 'notistack';

import useToken from '../contexts/token';
import fetchWithAuth from '../helpers/fetchWithAuth';

export default function usePlanetList(shouldUpdate) {
	const { token } = useToken();
	const [planetsList, setPlanetsList] = useState([]);
	const { enqueueSnackbar } = useSnackbar(); 

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

	useEffect(() => {
		let timeout = setTimeout(() => {
			for(const planet of planetsList) {
	            const warehouseCapacity = planet.buildingsCapacities['Warehouse'].quantity;

	            for(const building in planet.buildingsLevels) {
	                var prod = planet.buildingsCapacities[building];
	                var resource = planet.resourcesQuantities[prod.resource];
	                if (resource) {
	                	planet.resourcesQuantities[prod.resource] += prod.quantity;

	                    if (planet.resourcesQuantities[prod.resource] > warehouseCapacity) {
	                        planet.resourcesQuantities[prod.resource] = warehouseCapacity;
	                    }

	                }
	            }
	        }
	        setPlanetsList(JSON.parse(JSON.stringify(planetsList))); // TODO: improve that please ...
		}, 1000);

		return () => clearTimeout(timeout);
	}, [planetsList]);

	return planetsList;
}