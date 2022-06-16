using L3Projet.Business.Interfaces;
using L3Projet.Common.Models;
using L3Projet.DataAccess;

namespace L3Projet.Business.Implementations {
	public class UsersService : IUsersService {
		private readonly GameContext _gameContext;

		public UsersService(GameContext context) {
			this._gameContext = context;
		}

		public IEnumerable<User> GetAllUsers() {
			return _gameContext.Users.OrderBy(x => x.Id);
		}
	}
}
