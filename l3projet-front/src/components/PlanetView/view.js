import BuildingList from '../BuildingList';
import ResourceList from '../ResourceList';

import './index.css';

export default function PlanetView({ planet }) {
	return (
		<div>
			<span>{planet.name}</span>
			<div>
				<ResourceList resources={planet.resourcesQuantities} />
				<BuildingList buildings={planet.buildingsLevels} planetId={planet.id} />
			</div>
		</div>
	)
}
