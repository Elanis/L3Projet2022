import { useState } from 'react';

import LoginForm from '../LoginForm';
import RegisterForm from '../RegisterForm';

import './index.css';

export default function HomePage() {
	const [isLoginIn, setIsLoginIn] = useState(true);

	let form = (<RegisterForm switchForm={() => setIsLoginIn(true)} />);
	if(isLoginIn) {
		form = (<LoginForm switchForm={() => setIsLoginIn(false)} />);
	}

	return form;
}