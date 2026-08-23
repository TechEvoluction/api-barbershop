namespace Barbershop.Shareable.Config;

public class AuthenticationConfig
{
    public string Key { get; init; } = default!;
    public string UrlAuthentication { get; init; } = default!;
    public List<string> Scopes { get; init; } = default!;
    public int ExpiresInHours { get; init; } = default!;
    public string Issuer { get; init; } = default!;
    public string Audience { get; init; } = default!;
}