import './index.css';

export default function ResourceList({ resources }) {
	const resourcesDOM = [];

	for(const name in resources) {
		resourcesDOM.push(
			<div key={name}>
				<span className="resources-name">{name}</span>
				<span>{Math.round(resources[name])}</span>
			</div>
		)
	}

	return (
		<fieldset className='planet-panel'>
			<legend>Resources</legend>

			{resourcesDOM}
		</fieldset>
	);
}
