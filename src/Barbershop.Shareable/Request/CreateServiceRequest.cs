using Barbershop.Shareable.Enum;
using MediatR;
using OperationResult;

namespace Barbershop.Shareable.Request;

public record CreateServiceRequest(
    string Name,
    string Description,
    decimal Price,
    TimeInMinutes Duration,
    byte[]? Image) : IRequest<Result>;