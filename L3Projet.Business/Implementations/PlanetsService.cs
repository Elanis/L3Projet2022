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

            var warehouseCapacity = p.Buildings.ElementAt((int)BuildingType.Warehouse).Production.Item2;

            foreach (var building in p.Buildings) {
                var prod = building.Production;
                var resource = p.Resources.FirstOrDefault(r => r.Type == prod.Item1);
                if (resource != null) {
                    p.Resources.ElementAt((int)resource.Type).Quantity += prod.Item2 * diff.TotalSeconds;

                    if (p.Resources.ElementAt((int)resource.Type).Quantity > warehouseCapacity) {
                        p.Resources.ElementAt((int)resource.Type).Quantity = warehouseCapacity;
                    }

                }
            }

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
