namespace Barbershop.Shareable.Config;

public static class Roles
{
    public const string Admin = "admin";
    public const string Barber = "barber";
    public const string Customer = "customer";

    public static string[] GetAllRoles()
        => [Admin, Barber, Customer];
}