using MediatR;
using OperationResult;

namespace Barbershop.Shareable.Request.Auth;

public record ForgetPasswordRequest(string Email) : IRequest<Result>;