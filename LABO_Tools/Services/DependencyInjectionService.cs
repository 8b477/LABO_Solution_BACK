using LABO_DAL.Repositories;
using LABO_Tools.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

namespace LABO_Tools.Services
{
    public static class DependencyInjectionService
    {
        public static void ConfigureDependencyInjection(IServiceCollection services, IConfiguration configuration)
        {
            // Lire la chaîne de connexion depuis appsettings.json
            string connectionString = configuration.GetConnectionString("dev");


            // Injecter la connexion par défaut pour chaque utilisation
            services.AddScoped<UserRepo>(provider => new UserRepo(new SqlConnection(connectionString)));

            services.AddScoped<ProjetRepo>(provider => new ProjetRepo(new SqlConnection(connectionString)));


            // FILTRE
            services.AddScoped<CancellationFilter>();

        }
    }

}
