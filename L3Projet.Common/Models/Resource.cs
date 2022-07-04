namespace L3Projet.Common.Models {
    public class Resource {
        public ResourceType Type { get; set; }
        public double Quantity { get; set; }
        public Guid PlanetId { get; set; }
    }
}
