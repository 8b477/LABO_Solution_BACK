
namespace LABO_Tools.CustomError.ModelCustomError
{
    public class ModelError
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Details { get; set; }


        /// <summary>
        /// Initialise une nouvelle instance de la classe ModelError avec le code d'erreur, le message d'erreur et des détails facultatifs.
        /// </summary>
        /// <param name="errorCode">Le code d'erreur associé à cette erreur.</param>
        /// <param name="errorMessage">Le message d'erreur descriptif.</param>
        /// <param name="details">Des détails supplémentaires sur l'erreur (facultatif).</param>
        public ModelError(int errorCode, string errorMessage, string details = "")
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Details = details;
        }

    }
}
