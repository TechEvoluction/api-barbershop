using MediatR;
using OperationResult;
using System.Text.Json.Serialization;

namespace Barbershop.Shareable.Request.Barber;

public record AcceptBarberInvitationRequest(
    [property: JsonIgnore] Guid BarberId,
    string Token,
    string Password,
    string? Biography)
    : IRequest<Result>;