using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace L3Projet.Common.Models {
    public record BuildingCapacity {
        public string Resource { get; set; }
        public double Quantity { get; set; }
    }

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
        [NotMapped]
        public Dictionary<BuildingType, BuildingCapacity> BuildingsCapacities {
            get {
                return Buildings.ToDictionary(o => o.Type, o => new BuildingCapacity() { Resource = o.Production.Item1.ToString(), Quantity = o.Production.Item2 });
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
                    new Building { Type = BuildingType.House, Level = 1 },
                    new Building { Type = BuildingType.HQ, Level = 1 },
                },
                Resources = new HashSet<Resource> {
                    new Resource{ Type = ResourceType.Wood, Quantity = 100 },
                    new Resource{ Type = ResourceType.Metal, Quantity = 100 },
                    new Resource{ Type = ResourceType.Stone, Quantity = 100 },
                },
                LastCalculation = DateTime.UtcNow
            };
        }

        [NotMapped]
        public int Points { get => Buildings.Sum(b => b.Level); }
    }
}
