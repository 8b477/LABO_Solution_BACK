using LABO_DAL.Repositories;
using LABO_Tools.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

namespace LABO_Tools.Services
{
    /// <summary>
    /// Classe d'extension statique pour configurer l'injection de dépendances.
    /// </summary>

    public static class DependencyInjectionService
    {
        /// <summary>
        /// Configure l'injection de dépendances en enregistrant les services nécessaires dans la collection de services.
        /// </summary>
        /// <param name="services">La collection de services.</param>
        /// <param name="configuration">La configuration de l'application.</param>

        public static void ConfigureDependencyInjection(IServiceCollection services, IConfiguration configuration)
        {
            // Lire la chaîne de connexion depuis appsettings.json
            string? connectionString = configuration.GetConnectionString("dev");


            // Injecter la connexion par défaut pour chaque utilisation
            services.AddScoped<UserRepo>(provider => new UserRepo(new SqlConnection(connectionString)));
            services.AddScoped<ProjetRepo>(provider => new ProjetRepo(new SqlConnection(connectionString)));


            // Enregistre un filtre CancellationFilter pour être utilisé par les contrôleurs.
            services.AddScoped<CancellationFilter>();
            services.AddScoped<JwtUserIdentifiantFilter>();
        }
    }

}
