namespace Lyzer.Errors
{
    public class GeneralException : Exception
    {
        public GeneralException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; }
    }
}