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
                    // Politique pour les utilisateurs avec le rôle "Register"
                    options.AddPolicy("RequireRegisterRole", policy =>
                    {
                        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        policy.RequireAuthenticatedUser();
                        policy.RequireRole("Visiteur"); // Spécifie le rôle requis
                    });

                    // Politique pour les utilisateurs avec le rôle "Admin"
                    options.AddPolicy("RequireAdminRole", policy =>
                    {
                        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        policy.RequireAuthenticatedUser();
                        policy.RequireRole("Admin"); // Spécifie le rôle requis
                    });
                });
        }
    }    

}
