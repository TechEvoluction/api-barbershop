using MediatR;
using OperationResult;

namespace Barbershop.Shareable.Request.Auth;

public record ConfirmEmailRequest(string Email, string Token) : IRequest<Result>;