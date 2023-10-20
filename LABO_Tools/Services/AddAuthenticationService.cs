using LABO_Tools.Token;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LABO_Tools.Services
{
    /// <summary>
    /// Classe d'extension statique pour configurer les services d'authentification.
    /// </summary>

    public static class AddAuthenticationService
    {
        /// <summary>
        /// Configure les services d'authentification pour utiliser l'authentification JWT (JSON Web Tokens).
        /// </summary>
        /// <param name="services">La collection de services.</param>

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = TokenHelper.SIGNING_KEY,
                            //ValidIssuer = "http://localhost",
                            //ValidAudience = "http://localhost"
                        };
                    });
        }
    }

}