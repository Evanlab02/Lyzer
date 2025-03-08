namespace Lyzer.Errors
{
    public class CustomHttpException : Exception
    {
        public CustomHttpException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; }
    }
}