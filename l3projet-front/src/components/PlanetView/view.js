import BuildingList from '../BuildingList';
import ResourceList from '../ResourceList';

import './index.css';

export default function PlanetView({ planet }) {
	return (
		<div className="planet-view">
			<span>{planet.name}</span>
			<div>
				<ResourceList
					resources={planet.resourcesQuantities}
					maxCapacity={planet.buildingsCapacities['Warehouse'].quantity}
				/>
				<BuildingList buildings={planet.buildingsLevels} planetId={planet.id} />
			</div>
		</div>
	)
}
