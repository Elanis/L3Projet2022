using L3Projet.Common;
using L3Projet.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace L3Projet.DataAccess {
    public class GameContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Planet> Planets { get; set; }

        private readonly string SqlConnectionString;

        public GameContext(IOptions<AppSettings> settings) {
            SqlConnectionString = settings.Value.SqlConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
                 => options.UseNpgsql(SqlConnectionString);
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Building>()
                .HasKey(o => new { o.Type, o.PlanetId });
            modelBuilder.Entity<Resource>()
                .HasKey(o => new { o.Type, o.PlanetId });
        }
    }
}
