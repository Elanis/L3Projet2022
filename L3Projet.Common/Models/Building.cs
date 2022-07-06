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
            { BuildingType.Warehouse, (ResourceType.Storage, 9_876 ) }
        };

        [NotMapped]
        public (ResourceType, double) Production {
            get => (Capacities[Type].Item1, Capacities[Type].Item2 * Level);
        }
    }
}
