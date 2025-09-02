namespace Lyzer.Errors
{
    public class GeneralException(string message, int statusCode) : Exception(message)
    {
        public int StatusCode { get; } = statusCode;
    }
}