namespace Barbershop.Shareable.Extensions;

public static class DateTimeExtensions
{
    public static DateTime BrazilDateTime()
        => DateTime.UtcNow.AddHours(-3);
}