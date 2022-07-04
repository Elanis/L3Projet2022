using L3Projet.Common.Models;

namespace L3Projet.Business.Interfaces {
    public interface IPlanetsService {
        IEnumerable<Planet>? GetMyPlanets(string username);
        void Upgrade(string username, Guid id, BuildingType type);
    }
}
