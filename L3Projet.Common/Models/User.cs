namespace L3Projet.Common.Models {
    public class User {
        public Guid Id { get; init; }
        public string Username { get; init; }
        public string Password { get; set; }
        public string Mail { get; init; }
        public List<Planet> Planets { get; init; }
    }
}
