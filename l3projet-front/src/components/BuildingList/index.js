import { useSnackbar } from 'notistack';

import upgradeBuilding from '../../queries/upgradeBuilding';

import usePlanetList from '../../contexts/planetList';
import useToken from '../../contexts/token';

import BuildingListView from './view.js';

export default function BuildingList(props) {
	const { enqueueSnackbar } = useSnackbar();
	const { token } = useToken();
	const { requestPlanetListUpdate } = usePlanetList();

	return (
		<BuildingListView
			{...props}
			enqueueSnackbar={enqueueSnackbar}
			requestPlanetListUpdate={requestPlanetListUpdate}
			token={token}
			upgradeBuilding={upgradeBuilding}
		/>
	);
}
