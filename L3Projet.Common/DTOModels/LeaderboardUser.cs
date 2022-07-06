using L3Projet.Common.Models;

namespace L3Projet.Common.DTOModels {
    public class LeaderboardUser {
        public string Name { get; set; }
        public int Points { get; set; }

        public static LeaderboardUser MapUser(User user) {
            return new LeaderboardUser {
                Name = user.Username,
                Points = user.Points,
            };
        }
    }
}
