using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace LABO_Tools.Middleware
{
    public class AuthorizeAllEndpointsMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizeAllEndpointsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Vérifiez si l'endpoint est marqué comme public
            // (Ajoutez l'attribut [AllowAnonymous] sur les endpoints qui doivent rester publics)
            bool isPublicEndpoint = context.GetEndpoint()?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;

            if (!isPublicEndpoint)
            {
                context.Request.Headers["Authorization"] = "Bearer"; // Ajoutez le jeton ici
            }

            await _next(context);
        }
    }

}
