using L3Projet.Business.Interfaces;
using L3Projet.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace L3Projet.WebAPI.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase {
		private readonly IUsersService usersService;

		public UserController(IUsersService usersService) {
			this.usersService = usersService;
		}

		[HttpGet("/all")]
		public ActionResult<IEnumerable<User>> GetAll() {
			var users = usersService.GetAllUsers();

			if (users.Any()) {
				return Ok(users);
			}

			return NoContent();
		}
	}
}
