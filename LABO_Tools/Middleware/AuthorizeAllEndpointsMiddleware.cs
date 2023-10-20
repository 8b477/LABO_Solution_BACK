using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace LABO_Tools.Middleware
{
    public class AuthorizeAllEndpointsMiddleware
    {
        #region Constructeur

        #region Fields

        private readonly RequestDelegate _next;

        #endregion

        /// <summary>
        /// Initialise une nouvelle instance de la classe AuthorizeAllEndpointsMiddleware avec le RequestDelegate.
        /// </summary>
        /// <param name="next">La prochaine étape du pipeline de requête.</param>
 
        public AuthorizeAllEndpointsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion


        /// <summary>
        /// Vérifie si un endpoint est marqué comme public (avec l'attribut [AllowAnonymous]).
        /// Si l'endpoint n'est pas public, ce middleware ajoute un en-tête "Authorize" avec la valeur "RequireToken" à la requête.
        /// Cela est utilisé pour forcer l'authentification sur les endpoints non publics.
        /// </summary>
        /// <param name="context">Le contexte de la requête HTTP.</param>

        public async Task Invoke(HttpContext context)
        {
            // Vérifiez si l'endpoint est marqué comme public
            // (Ajoutez l'attribut [AllowAnonymous] sur les endpoints qui doivent rester publics)
            bool isPublicEndpoint = context.GetEndpoint()?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;

            if (!isPublicEndpoint)
            {
                context.Request.Headers["Authorize"] = "RequireToken"; // Ajoutez le jeton ici
            }

            // Passe la requête au middleware suivant dans le pipelin
            await _next(context);
        }

    }
}
