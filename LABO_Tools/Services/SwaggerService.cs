using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace LABO_Tools.Services
{
    /// <summary>
    /// Classe d'extension statique pour configurer Swagger (OpenAPI).
    /// </summary>

    public static class SwaggerService
    {

        /// <summary>
        /// Configure Swagger (OpenAPI) en ajoutant des informations sur l'API, les schémas de sécurité, etc.
        /// </summary>
        /// <param name="services">La collection de services.</param>

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API CrowdFunding",
                    Version = "v1",
                    Description = "Cette API permet de connecter des gens ensemble pour concrétiser de beaux projets !",
                });

                // Ajoute la définition de sécurité pour JWT (Bearer token)
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                });

                // Ajoute l'exigence de sécurité pour JWT
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
             });
        }
    }
}
