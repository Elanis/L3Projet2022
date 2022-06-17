export default function RegisterForm({ switchForm }) {
	return (
		<div className="homepage-form">
			<h2 className="homepage-title">Register</h2>
			<input type="text" placeholder="Username" />
			<input type="password" placeholder="Password" />
			<input type="password" placeholder="Password Confirmation" />
			<input type="password" placeholder="Mail" />
			<input type="button" value="Register" />
			<br/>
			<span className="homepage-link" onClick={switchForm}>Already have an account ?</span>
		</div>
	);
}