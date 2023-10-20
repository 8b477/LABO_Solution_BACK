using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace LABO_Tools.Services
{
    /// <summary>
    /// Classe d'extension statique pour configurer les politiques d'autorisation dans ASP.NET Core.
    /// </summary>

    public static class AddAuthorizationService
    {

        /// <summary>
        /// Configure les politiques d'autorisation pour spécifier comment les utilisateurs sont autorisés à accéder aux ressources de l'application.
        /// </summary>
        /// <param name="services">La collection de services.</param>

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireToken", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireAssertion(context => true); // Autorise toutes les requêtes si le token est correct

                    // NICE HAVE :
                    // Ajouter une logique plus restrictive pour différencier les user qui créée un projet et ceux qui ne font que participer via des donation et ainsi ajuster les droits.
                });
            });
        }

    }    

}
