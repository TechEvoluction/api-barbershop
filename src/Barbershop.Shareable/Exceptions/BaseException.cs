using System.Net;

namespace Barbershop.Shareable.Exceptions;

public class BaseException : Exception
{
    public string Error { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public HttpStatusCode StatusCode { get; init; }

    public BaseException(string message, string code, int httpStatusCode)
        : base(message)
    {
        Error = message;
        Code = code;
        StatusCode = (HttpStatusCode)httpStatusCode;
    }
}