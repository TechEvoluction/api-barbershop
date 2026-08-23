using MediatR;
using OperationResult;

namespace Barbershop.Shareable.Request.Auth;

public record ResetPasswordRequest(string Email, string Token, string NewPassword) : IRequest<Result>;