using L3Projet.Business.Implementations;
using L3Projet.Business.Interfaces;
using L3Projet.DataAccess.Implementations;
using L3Projet.DataAccess.Interfaces;
using L3Projet.WebAPI.HealthCheck;

namespace L3Projet.WebAPI {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		private const string CORS_POLICY = "CORS_POLICY";

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			// Add services to the container.
			services.AddSingleton<IUsersDataAccess, UserDataAccess>();
			services.AddTransient<IUsersService, UsersService>();

			services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			services.AddCors(options => {
				options.AddPolicy(name: CORS_POLICY,
								  policy => {
									  policy.WithOrigins("http://localhost:3000")
											.AllowAnyMethod()
											.AllowAnyHeader();
								  });
			});

			services.AddHealthChecks()
				.AddCheck<DbHealthCheck>("Database");
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			// Configure the HTTP request pipeline.
			if (env.IsDevelopment()) {
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseRouting();

			app.UseCors(CORS_POLICY);

			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");

				endpoints.MapHealthChecks("/health");
			});
		}
	}
}
