namespace L3Projet.Common.DTOModels {
	public class UserRegistrationRequest {
		public string Username { get; set; }
		public string Password { get; set; }
		public string ConfirmationPassword { get; set; }
		public string Mail { get; set; }
	}
}
