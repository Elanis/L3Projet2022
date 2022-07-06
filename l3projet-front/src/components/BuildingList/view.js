import './index.css';

export default function BuildingList({ buildings, enqueueSnackbar, planetId, requestPlanetListUpdate, token, upgradeBuilding,  }) {
	const buildingsDOM = [];

	// TODO: upgrade button onClick
	// TODO: upgrade costs
	for(const name in buildings) {
		buildingsDOM.push(
			<div class="planet-building-panel">
				<span className="building-name">{name}</span>
				<span className="building-lvl">{buildings[name]}</span>
				<input
					className="building-upgrade"
					type="button"
					value="Upgrade"
					onClick={() => upgradeBuilding(token, enqueueSnackbar, requestPlanetListUpdate, planetId, Object.keys(buildings).indexOf(name))}
				/>
			</div>
		)
	}

	return (
		<fieldset className='planet-panel'>
			<legend>Buildings</legend>
			{buildingsDOM}
		</fieldset>);
}