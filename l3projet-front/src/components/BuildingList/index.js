import { useSnackbar } from 'notistack';

import upgradeBuilding from '../../queries/upgradeBuilding';
import useToken from '../../contexts/token';

import BuildingListView from './view.js';

export default function BuildingList(props) {
	const { enqueueSnackbar } = useSnackbar();
	const { token } = useToken();

	return (
		<BuildingListView
			{...props}
			upgradeBuilding={upgradeBuilding}
			enqueueSnackbar={enqueueSnackbar}
			token={token}
		/>
	);
}
