namespace L3Projet.Common.Models {
    public class Building {
        public BuildingType Type { get; set; }
        public int Level { get; set; }
        public Guid PlanetId { get; set; }
    }
}
