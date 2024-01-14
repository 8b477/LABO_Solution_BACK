
using LABO_BLL.ExceptionModel;

namespace LABO_Tools.CustomError.ExtensionCustomError
{
    public static class ExceptionExtensions
    {
        public static void ThrowMyAppException(this Exception exception, int error, string message)
        {
            throw new MyAppException(error, message);
        }
    }
}
