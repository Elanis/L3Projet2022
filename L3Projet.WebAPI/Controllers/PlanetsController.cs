using L3Projet.Business.Interfaces;
using L3Projet.Common;
using L3Projet.Common.DTOModels;
using L3Projet.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace L3Projet.WebAPI.Controllers {
    [Authorize(Policy = AuthPolicies.JWT_POLICY)]
    [ApiController]
    [Route("[controller]")]
    public class PlanetsController : ControllerBase {
        private readonly IPlanetsService planetsService;

        public PlanetsController(IPlanetsService planetsService) {
            this.planetsService = planetsService;
        }

        [HttpGet("mine")]
        public ActionResult<IEnumerable<Planet>> GetMyPlanets() {
            var planets = planetsService.GetMyPlanets(HttpContext.User.Identity.Name);

            if (planets == null) {
                return NoContent();
            }

            return Ok(planets);
        }

        [HttpPost("upgrade")]
        public ActionResult Upgrade(BuildingUpgradeRequest request) {
            planetsService.Upgrade(HttpContext.User.Identity.Name, request.Id, (BuildingType)request.Type);

            return Ok();
        }
    }
}
