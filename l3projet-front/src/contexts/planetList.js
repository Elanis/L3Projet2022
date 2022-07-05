import { createContext, useContext, useState } from 'react';

import usePlanetList from '../hooks/usePlanetList';

const PlanetListContext = createContext();

export const PlanetListProvider = ({ children }) => {
	const [shouldUpdate, setShouldUpdate] = useState(0);
	const planetList = usePlanetList(shouldUpdate);

	console.log(shouldUpdate);
	console.log(planetList);

	return (
		<PlanetListContext.Provider value={{ planetList, requestPlanetListUpdate: () => setShouldUpdate(Date.now()) }}>
			{children}
		</PlanetListContext.Provider>
	);
};

export default function usePlanetListFromContext() {
	const { planetList, requestPlanetListUpdate } = useContext(PlanetListContext);

	return { planetList, requestPlanetListUpdate };
}