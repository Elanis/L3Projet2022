using System.ComponentModel.DataAnnotations.Schema;

namespace L3Projet.Common.Models {
    public class Building {
        public BuildingType Type { get; set; }
        public int Level { get; set; }
        public Guid PlanetId { get; set; }

        public static readonly Dictionary<BuildingType, (ResourceType, double)> Capacities = new() {
            { BuildingType.Metallurgy, (ResourceType.Metal, 8.3) },
            { BuildingType.SawMill, (ResourceType.Wood, 7.8) },
            { BuildingType.Quarry, (ResourceType.Stone, 5.6) },
            { BuildingType.Warehouse, (ResourceType.Storage, 9_876 ) },
            { BuildingType.House, (ResourceType.None, 0) },
            { BuildingType.HQ, (ResourceType.None, 0) },
        };

        [NotMapped]
        public (ResourceType, double) Production {
            get => (Capacities[Type].Item1, Capacities[Type].Item2 * Level);
        }

        [NotMapped]
        public static readonly Dictionary<BuildingType, Dictionary<ResourceType, double>> Costs = new() {
            { BuildingType.SawMill, new Dictionary<ResourceType, double>() {
                    { ResourceType.Wood, 100 },
                    { ResourceType.Metal, 75 },
                    { ResourceType.Stone, 125 },
                }
            },
            { BuildingType.Metallurgy, new Dictionary<ResourceType, double>() {
                    { ResourceType.Wood, 50 },
                    { ResourceType.Metal, 75 },
                    { ResourceType.Stone, 150 },
                }
            },
            { BuildingType.Quarry, new Dictionary<ResourceType, double>() {
                    { ResourceType.Wood, 125 },
                    { ResourceType.Metal, 75 },
                    { ResourceType.Stone, 50 },
                }
            },
            { BuildingType.Warehouse, new Dictionary<ResourceType, double>() {
                    { ResourceType.Wood, 125 },
                    { ResourceType.Metal, 200 },
                    { ResourceType.Stone, 175 },
                }
            },
            { BuildingType.House, new Dictionary<ResourceType, double>() {
                    { ResourceType.Wood, 125 },
                    { ResourceType.Metal, 150 },
                    { ResourceType.Stone, 175 },
                }
            },
            { BuildingType.HQ, new Dictionary<ResourceType, double>() {
                    { ResourceType.Wood, 125 },
                    { ResourceType.Metal, 150 },
                    { ResourceType.Stone, 325 },
                }
            }
        };

        [NotMapped]
        public Dictionary<ResourceType, double> Cost { get => Costs[Type]; }
    }
}
