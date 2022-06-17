import { Routes, Route } from 'react-router-dom';

import HomePage from '../HomePage';

export default function App() {
	return (
		<Routes>
			<Route path="/" element={<HomePage />} />
			<Route path="game">
				<Route index element={<div>Game home</div>} />
				<Route path="building/:buildingId" element={<div>Building view</div>} />
			</Route>
		</Routes>
	);
}