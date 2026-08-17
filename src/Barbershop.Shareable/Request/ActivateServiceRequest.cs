using MediatR;
using OperationResult;

namespace Barbershop.Shareable.Request;

public record ActivateServiceRequest(Guid Id) : IRequest<Result>;