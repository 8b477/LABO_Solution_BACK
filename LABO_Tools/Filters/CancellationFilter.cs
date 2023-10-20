using LABO_Tools.CustomError.ExtensionCustomError;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace LABO_Tools.Filters
{
    public class CancellationFilter : IActionFilter
    {

        #region Constructeur

        #region Fields
        private readonly ILogger<CancellationFilter> _logger; 
        #endregion

        public CancellationFilter(ILogger<CancellationFilter> logger)
        {
            _logger = logger;
        } 

        #endregion


        /// <summary>
        /// Cette méthode est exécutée après qu'une action a été exécutée par le contrôleur.
        /// Elle vérifie si la requête a été annulée (c'est-à-dire si le client a annulé la requête).
        /// Si la requête a été annulée, elle génère une réponse d'erreur personnalisée avec un code d'erreur 500
        /// et un message spécifique.
        /// </summary>
        /// <param name="context">Le contexte de l'exécution de l'action.</param>

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Vérifie si la requête a été annulée par le client
            if (context.HttpContext.RequestAborted.IsCancellationRequested)
            {
                // Ajoute des informations de journalisation supplémentaires (nom du contrôleur et de l'action)
                var controllerName = context.RouteData.Values["controller"];
                var actionName = context.RouteData.Values["action"];
                var message = "La requête a été annulée dans le contrôleur : " + controllerName + ", action : " + actionName;

                // Utilise le logger pour écrire le message
                _logger.LogError(message);

                // Génère une réponse d'erreur personnalisée
                context.HandleCustomError(500, "Votre requête a été annulée !");
            }
        }

        // Cette méthode est exécutée avant que l'action ne soit exécutée, on peut y ajouter des logiques si nécessaire.
        public void OnActionExecuting(ActionExecutingContext context)
        {
           // throw new NotImplementedException();
        }
    }
}
