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

            // Todo: time

            // Remove resources
            var building = planet.Buildings.ElementAt((int)type);
            var prevLevel = building.Level;
            foreach (var resource in planet.Resources) {
                if (resource.Quantity < prevLevel * building.Cost[resource.Type]) {
                    throw new InvalidDataException("No enough resources");
                }
            }
            foreach (var resource in planet.Resources) {
                resource.Quantity -= prevLevel * building.Cost[resource.Type];
            }

            planet.Buildings.ElementAt((int)type).Level += 1;

            context.SaveChanges();
        }
    }
}
