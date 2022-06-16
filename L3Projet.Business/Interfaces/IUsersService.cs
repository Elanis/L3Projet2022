using L3Projet.Common.Models;

namespace L3Projet.Business.Interfaces {
	public interface IUsersService {
		IEnumerable<User> GetAllUsers();
	}
}
