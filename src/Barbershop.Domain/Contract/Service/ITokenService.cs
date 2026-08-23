using Barbershop.Domain.Entity;
using System.Security.Claims;

namespace Barbershop.Domain.Contract.Service;

public interface ITokenService
{
    string GenerateJwtToken(UserEntity user, string[] roles);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalToken(string token);
}