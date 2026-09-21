using Barbershop.Shareable.Enum;
using Barbershop.Shareable.Response;
using MediatR;
using OperationResult;

namespace Barbershop.Shareable.Request.Barber;

public record BarberInvitationRequest(string Cpf, string Email, string Fullname, string PhoneNumber, DateOnly DateOfBirth, Gender? Gender)
    : IRequest<Result<CreateBarberResponse>>;