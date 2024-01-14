namespace LABO_BLL.ExceptionModel
{
    public class MyAppException : Exception
    {

        public int Error { get; private set; }

        public MyAppException(int error, string message) : base(message)
        {
            Error = error;
        }

    }
}
