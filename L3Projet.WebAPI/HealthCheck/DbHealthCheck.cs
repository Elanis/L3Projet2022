using L3Projet.DataAccess;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace L3Projet.WebAPI.HealthCheck {
    public class DbHealthCheck : IHealthCheck {
        private readonly GameContext gamecontext;
        public DbHealthCheck(GameContext context) {
            this.gamecontext = context;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default) {
            try {
                gamecontext.Users.Any();

                return Task.FromResult(
                    HealthCheckResult.Healthy("OK")
                );
            } catch {
                return Task.FromResult(
                    new HealthCheckResult(
                        context.Registration.FailureStatus,
                        "Error"
                    )
                );
            }
        }
    }
}
