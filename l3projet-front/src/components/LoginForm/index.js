export default function LoginForm({ switchForm }) {
	return (
		<div className="homepage-form">
			<h2 className="homepage-title">Login</h2>
			<input type="text" placeholder="Username" />
			<input type="password" placeholder="Password" />
			<input type="button" value="Login" />
			<br/>
			<span className="homepage-link" onClick={switchForm}>No account yet ? Register now !</span>
		</div>
	);
}