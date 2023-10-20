using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using LABO_Tools.CustomError.ModelCustomError;

namespace LABO_Tools.CustomError.ExtensionCustomError
{
    public static class ActionExecutedContextExtensions
    {

        /// <summary>
        /// Gère une erreur perso pour l'action exécutée en remplaçant le résultat de l'action par ma réponse d'erreur 
        /// 
        /// </summary>
        /// <param name="context">Le contexte de l'action exécutée.</param>
        /// <param name="errorCode">Le code d'erreur personnalisé à associer à la réponse.</param>
        /// <param name="errorMessage">Le message d'erreur descriptif à inclure dans la réponse.</param>
        /// <param name="details">Des détails optionnels supplémentaires concernant l'erreur.</param>

        public static void HandleCustomError(this ActionExecutedContext context, int errorCode, string errorMessage, string details = "")
        {
            var customError = new ModelError(errorCode, errorMessage, details);
            context.Result = new ObjectResult(customError)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }

}
