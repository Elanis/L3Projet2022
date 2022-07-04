using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace L3Projet.Common.Models {
    public class Planet {
        public Guid Id { get; init; }
        public string Name { get; set; }
        [JsonIgnore]
        public HashSet<Building> Buildings { get; init; }
        [NotMapped]
        public Dictionary<BuildingType, int> BuildingsLevels {
            get {
                return Buildings.ToDictionary(o => o.Type, o => o.Level);
            }
        }

        [JsonIgnore]
        public HashSet<Resource> Resources { get; init; }
        [NotMapped]
        public Dictionary<ResourceType, double> ResourcesQuantities {
            get {
                return Resources.ToDictionary(o => o.Type, o => o.Quantity);
            }
        }

        public DateTime LastCalculation { get; set; }

        public static Planet CreateEmptyPlanet(string username) {
            return new Planet() {
                Id = new Guid(),
                Name = $"{username}'s planet",
                Buildings = new HashSet<Building> {
                    new Building { Type = BuildingType.SawMill, Level = 1 },
                    new Building { Type = BuildingType.Metallurgy, Level = 1 },
                    new Building { Type = BuildingType.Quarry, Level = 1 },
                    new Building { Type = BuildingType.Warehouse, Level = 1 },
                },
                Resources = new HashSet<Resource> {
                    new Resource{ Type = ResourceType.Wood, Quantity = 100 },
                    new Resource{ Type = ResourceType.Metal, Quantity = 100 },
                    new Resource{ Type = ResourceType.Stone, Quantity = 100 },
                },
                LastCalculation = DateTime.UtcNow
            };
        }
    }
}
