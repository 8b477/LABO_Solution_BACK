using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;


namespace LABO_Tools.Filters
{

    /// <summary>
    /// Filtre d'action pour extraire l'identifiant de l'utilisateur à partir du jeton JWT.
    /// Stocke ensuite cet identifiant dans les objets HttpContext.Items pour le rendre accessible aux actions ultérieures de la même requête si nécessaire.
    /// </summary>

    public class JwtUserIdentifiantFilter : IActionFilter
    {

        /// <summary>
        /// Cette méthode est exécutée avant l'exécution de l'action.
        /// Elle extrait l'identifiant de l'utilisateur du jeton JWT et le stocke dans HttpContext.Items.
        /// </summary>
        /// <param name="context">Le contexte d'exécution de l'action.</param>

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Extrait l'identifiant de l'utilisateur du jeton JWT
            int identifiant = int.Parse(context.HttpContext.User.FindFirst(ClaimTypes.Name).Value);

            // Stocke l'identifiant dans l'objet context.HttpContext.Items pour une utilisation ultérieure dans la même requête.
            context.HttpContext.Items["identifiant"] = identifiant;
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Peut contenir du code supplémentaire à exécuter après l'exécution de l'action.
        }

    }
}
