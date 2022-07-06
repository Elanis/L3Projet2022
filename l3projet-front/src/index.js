import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import { SnackbarProvider } from 'notistack';

import { LeaderboardProvider } from './contexts/leaderboard';
import { PlanetListProvider } from './contexts/planetList';
import { TokenProvider } from './contexts/token';

import App from './components/App';

import reportWebVitals from './reportWebVitals';

import './index.css';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
	<React.StrictMode>
		<TokenProvider>
			<SnackbarProvider maxSnack={3}>
				<PlanetListProvider>
					<LeaderboardProvider>
						<BrowserRouter>
							<App />
						</BrowserRouter>
					</LeaderboardProvider>
				</PlanetListProvider>
			</SnackbarProvider>
		</TokenProvider>
	</React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
