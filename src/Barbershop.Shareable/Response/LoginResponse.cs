using System.Text.Json.Serialization;

namespace Barbershop.Shareable.Response;

public record LoginResponse(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("refresh_token")] string RefreshToken);