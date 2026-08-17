namespace Barbershop.Shareable.Exceptions;

public class AppException : BaseException
{
    public AppException(
        string message,
        string code,
        int httpStatusCode = 422
    ) : base(message, code, httpStatusCode) { }
}