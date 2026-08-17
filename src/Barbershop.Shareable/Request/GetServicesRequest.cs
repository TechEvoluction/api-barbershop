using Barbershop.Shareable.Response;
using MediatR;
using OperationResult;

namespace Barbershop.Shareable.Request;

public record GetServicesRequest() : IRequest<Result<ServiceResponse[]>>;