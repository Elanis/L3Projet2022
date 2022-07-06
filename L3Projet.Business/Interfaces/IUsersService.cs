using L3Projet.Common.DTOModels;

namespace L3Projet.Business.Interfaces {
	public interface IUsersService {
		string? AuthenticateUser(string username, string password);
		string? Register(UserRegistrationRequest request);
		UserPublicDataResponse? GetByUsername(string? name);
        object RenewToken(string? name);
    }
}
