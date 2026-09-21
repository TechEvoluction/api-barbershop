using Barbershop.Domain.Contract.Service;
using Barbershop.Domain.Entity;
using Barbershop.Shareable.Config;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Barbershop.Domain.Service;

public class TokenService : ITokenService
{
    private readonly AuthenticationConfig _config;

    public TokenService(AppConfig config) => _config = config.Authentication;

    public string GenerateJwtToken(UserEntity user, string[] roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config.Key);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Name, user.Fullname),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var scopes = roles.Contains(Roles.Customer)
            ? _config.Scopes.Where(s => !s.Contains("management"))
            : (roles.Contains(Roles.Barber) || roles.Contains(Roles.Admin))
                ? _config.Scopes
                : Enumerable.Empty<string>();
        
        foreach (var scope in scopes)
            claims.Add(new("scopes", scope));

        foreach (var role in roles)
            claims.Add(new(ClaimTypes.Role, role));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(_config.ExpiresInHours),
            Issuer = _config.Issuer,
            Audience = _config.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal GetPrincipalToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.Key)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(
            token,
            tokenValidationParameters,
            out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Token inválido.");
        }

        return principal;
    }
}