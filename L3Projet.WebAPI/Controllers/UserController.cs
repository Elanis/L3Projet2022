using L3Projet.Business.Interfaces;
using L3Projet.Common;
using L3Projet.Common.DTOModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace L3Projet.WebAPI.Controllers {
	[Authorize(Policy = AuthPolicies.JWT_POLICY)]
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase {
		private readonly IUsersService usersService;

		public UserController(IUsersService usersService) {
			this.usersService = usersService;
		}

		[AllowAnonymous]
		[HttpPost("/auth")]
		public ActionResult<string> AuthenticateUser(UserAuthenticationRequest authenticationRequest) {
			var token = usersService.AuthenticateUser(authenticationRequest.Username, authenticationRequest.Password);

			if (token == null) {
				return Unauthorized();
			}

			return Ok(token);
		}

		[AllowAnonymous]
		[HttpPost("/register")]
		public ActionResult<string> RegisterUser(UserRegistrationRequest registrationRequest) {
			var token = usersService.Register(registrationRequest);

			if (token == null) {
				return UnprocessableEntity();
			}

			return Ok(token);
		}

		[HttpGet("/current")]
		public ActionResult<UserPublicDataResponse> GetCurrentUser() {
			var user = usersService.GetByUsername(HttpContext?.User?.Identity?.Name);

			if (user == default) {
				return NoContent();
			}

			return Ok(user);
		}
	}
}
