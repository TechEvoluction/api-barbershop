using Barbershop.Shareable.Enum;
using MediatR;
using OperationResult;

namespace Barbershop.Shareable.Request.Auth;

public record RegisterUserRequest(string Cpf, string Email, string Password, string Fullname, string PhoneNumber, DateOnly DateOfBirth, Gender? Gender)
    : IRequest<Result>;