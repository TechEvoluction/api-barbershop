using MediatR;
using OperationResult;
using System.Text.Json.Serialization;

namespace Barbershop.Shareable.Request;

public record ServiceOnSaleRequest(
    [property: JsonIgnore] Guid Id,
    decimal promotionalPrice,
    DateTime? promotionalPriceEndDate) : IRequest<Result>;