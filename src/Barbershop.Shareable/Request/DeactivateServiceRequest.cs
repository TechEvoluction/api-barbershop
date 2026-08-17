using MediatR;
using OperationResult;

namespace Barbershop.Shareable.Request;

public record DeactivateServiceRequest(Guid Id) : IRequest<Result>;