using Barbershop.Shareable.Response;
using MediatR;
using OperationResult;

namespace Barbershop.Shareable.Request.Auth;

public record RefreshTokenRequest(string AccessToken, string RefreshToken) : IRequest<Result<LoginResponse>>;