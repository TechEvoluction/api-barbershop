using Barbershop.Domain.Contract.Service;
using Barbershop.Domain.Entity;
using Barbershop.Shareable.Exceptions;
using Barbershop.Shareable.Request.Auth;
using Barbershop.Shareable.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OperationResult;
using System.Security.Claims;

namespace Barbershop.Domain.Handler;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, Result<LoginResponse>>
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly ITokenService _tokenService;

    public RefreshTokenHandler(
        UserManager<UserEntity> userManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<Result<LoginResponse>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        // Valida principal access token expirado
        var principal = _tokenService.GetPrincipalToken(request.AccessToken);
        if (principal is null)
            return new AppException("Invalid access token", "ACCESS_TOKEN");

        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId!);

        if (user is null)
            return new NotFoundException("User");

        var sevedRefreshToken = await _userManager.GetAuthenticationTokenAsync(user, "Barbershop.Api", "refreshToken");
        if (sevedRefreshToken != request.RefreshToken)
            return new AppException("Invalid refresh token", "REFRESH_TOKEN");

        var roles = _userManager.GetRolesAsync(user);

        var token = _tokenService.GenerateJwtToken(user, [.. roles.Result]);
        var refreshToken = _tokenService.GenerateRefreshToken();

        await _userManager.SetAuthenticationTokenAsync(user, "Barbershop.Api", nameof(refreshToken), refreshToken);

        return new LoginResponse(token, refreshToken);
    }
}