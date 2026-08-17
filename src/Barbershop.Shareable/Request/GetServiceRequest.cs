using Barbershop.Shareable.Response;
using MediatR;
using OperationResult;

namespace Barbershop.Shareable.Request;

public record GetServiceRequest(Guid Id) : IRequest<Result<ServiceResponse>>;