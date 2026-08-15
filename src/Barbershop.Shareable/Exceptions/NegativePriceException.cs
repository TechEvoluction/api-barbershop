namespace Barbershop.Shareable.Exceptions;

public class NegativePriceException : BaseException
{
    public NegativePriceException(
        string message = "Preço não pode ser negativo ou zero.",
        string code = "INCORRECT_PRICE",
        int httpStatusCode = 422
    ) : base(message, code, httpStatusCode) { }
}