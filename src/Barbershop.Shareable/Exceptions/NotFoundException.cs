namespace Barbershop.Shareable.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string recurso)
        : base($"{recurso.ToUpper()} não encontrado.", "NOT_FOUND", 400) { }
}