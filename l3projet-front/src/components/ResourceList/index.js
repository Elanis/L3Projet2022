import './index.css';

export default function ResourceList({ maxCapacity, resources }) {
	const resourcesDOM = [];

	for(const name in resources) {
		const qty = Math.round(resources[name]);
		let qtyClass = '';
		if(qty === maxCapacity) {
			qtyClass = 'resources-max';
		}

		resourcesDOM.push(
			<div key={name}>
				<span className="resources-name">{name}</span>
				<span className={qtyClass}>{qty}</span>
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
