using Barbershop.Domain.Contract.Service;
using Barbershop.Domain.Entity;
using Barbershop.Shareable.Exceptions;
using Barbershop.Shareable.Request.Auth;
using Barbershop.Shareable.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OperationResult;

namespace Barbershop.Domain.Handler;

public class LoginHandler : IRequestHandler<LoginRequest, Result<LoginResponse>>
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly ITokenService _tokenService;

    public LoginHandler(
        UserManager<UserEntity> userManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<Result<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return new AppException("Invalid credentials", "INVALID_CREDENTIALS");

        if (user is { EmailConfirmed: false })
            return new AppException("Confirm your email to log in.", "EMAIL_CONFIRMED");

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
            return new AppException("Invalid credentials", "INVALID_CREDENTIALS");

        var roles = _userManager.GetRolesAsync(user);

        var token = _tokenService.GenerateJwtToken(user, [.. roles.Result]);
        var refreshToken = _tokenService.GenerateRefreshToken();

        await _userManager.SetAuthenticationTokenAsync(user, "Barbershop.Api", nameof(refreshToken), refreshToken);

        return new LoginResponse(token, refreshToken);
    }
}