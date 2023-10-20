

namespace LABO_Tools.CustomError.ModelCustomError
{
    public class ModelError
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Details { get; set; }

        public ModelError(int errorCode, string errorMessage, string details = "")
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Details = details;
        }
    }
}
