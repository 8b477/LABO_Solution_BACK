using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace LABO_Tools.Services
{
    public static class AddAuthorizationService
    {

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
