namespace Barbershop.Shareable.Exceptions;

public class SchedulingException : BaseException
{
    public SchedulingException(
        string message,
        string code,
        int httpStatusCode = 422
    ) : base(message, code, httpStatusCode) { }
}