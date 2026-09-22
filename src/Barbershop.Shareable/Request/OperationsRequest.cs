using Barbershop.Shareable.DTO;
using Barbershop.Shareable.Response;
using MediatR;
using OperationResult;

namespace Barbershop.Shareable.Request;

public record OperationsRequest(List<OperationDTO> Operations) : IRequest<Result<OperationsResponse>>;