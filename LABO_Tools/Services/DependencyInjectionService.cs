using LABO_DAL.Interfaces;
using LABO_DAL.Repositories;
using LABO_DAL.Services;
using LABO_DAL.Services.Interfaces;

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

            #region Fields
            string? connectionString = configuration.GetConnectionString("dev");
            #endregion


            #region Injection de dependance

            #region User
            services.AddScoped<IUserRepo, UserRepo>(provider => new UserRepo(new SqlConnection(connectionString)));
            #endregion

            #region Contrepartie
            services.AddScoped<IContrepartieRepo, ContrepartieRepo>(provider => new ContrepartieRepo(new SqlConnection(connectionString)));
            #endregion

            #region Projet
            services.AddScoped<IProjetService, ProjetService>();
            
            services.AddScoped<IProjetRepo, ProjetRepo>(provider => new ProjetRepo(new SqlConnection(connectionString)));
            #endregion

            #endregion


            #region Filtre
            services.AddScoped<CancellationFilter>();
            services.AddScoped<JwtUserIdentifiantFilter>(); 
            #endregion

        }
    }

}
