using Barbershop.Shareable.Response;
using MediatR;
using OperationResult;

namespace Barbershop.Shareable.Request.Auth;

public record LoginRequest(string Email, string Password) : IRequest<Result<LoginResponse>>;