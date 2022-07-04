using L3Projet.Business.Interfaces;
using L3Projet.Common.Models;
using L3Projet.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace L3Projet.Business.Implementations {
    public class PlanetsService : IPlanetsService {
        private readonly GameContext context;

        public PlanetsService(GameContext context) {
            this.context = context;
        }

        public IEnumerable<Planet>? GetMyPlanets(string username) {
            return context.Users
                .Include(x => x.Planets)
                .ThenInclude(x => x.Buildings)
                .FirstOrDefault((user) => user.Username == username)?.Planets;
        }
    }
}
