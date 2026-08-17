using Barbershop.Shareable.Enum;
using Barbershop.Shareable.Response;
using MediatR;
using OperationResult;

namespace Barbershop.Shareable.Request;

public record UpdateServiceRequest(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    TimeInMinutes Duration,
    byte[]? Image) : IRequest<Result<ServiceResponse>>;