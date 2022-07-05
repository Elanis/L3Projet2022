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

        private void UpdateResources(Planet p) {
            var now = DateTime.UtcNow;
            var diff = now - p.LastCalculation;

            // Todo: custom prod per type
            // Todo: warehouse storage

            p.Resources.ElementAt((int)ResourceType.Wood).Quantity += p.Buildings.ElementAt((int)BuildingType.SawMill).Level * diff.TotalSeconds;
            p.Resources.ElementAt((int)ResourceType.Metal).Quantity += p.Buildings.ElementAt((int)BuildingType.Metallurgy).Level * diff.TotalSeconds;
            p.Resources.ElementAt((int)ResourceType.Stone).Quantity += p.Buildings.ElementAt((int)BuildingType.Quarry).Level * diff.TotalSeconds;

            p.LastCalculation = now;
        }

        public IEnumerable<Planet>? GetMyPlanets(string username) {
            var planets = context.Users
                .Include(x => x.Planets)
                .ThenInclude(x => x.Buildings)
                .Include(x => x.Planets)
                .ThenInclude(x => x.Resources)
                .FirstOrDefault((user) => user.Username == username)?.Planets;

            if (planets == null) {
                return null;
            }

            planets.ForEach(UpdateResources);

            context.SaveChanges();

            return planets;
        }

        public void Upgrade(string username, Guid id, BuildingType type) {
            var planet = context.Users
                .Include(x => x.Planets)
                .ThenInclude(x => x.Buildings)
                .Include(x => x.Planets)
                .ThenInclude(x => x.Resources)
                .FirstOrDefault((user) => user.Username == username)
                ?.Planets
                .FirstOrDefault((p) => p.Id == id);

            if (planet == null) {
                throw new ArgumentException("Invalid planet");
            }

            UpdateResources(planet);

            // Todo: check enough resources
            // Todo: time
            // Todo: custom resources amount per building

            // Remove resources
            var prevLevel = planet.Buildings.ElementAt((int)type).Level;
            planet.Resources.ElementAt((int)ResourceType.Wood).Quantity -= prevLevel * 100;
            planet.Resources.ElementAt((int)ResourceType.Metal).Quantity -= prevLevel * 100;
            planet.Resources.ElementAt((int)ResourceType.Stone).Quantity -= prevLevel * 100;

            planet.Buildings.ElementAt((int)type).Level += 1;

            context.SaveChanges();
        }
    }
}
