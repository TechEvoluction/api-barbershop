using Barbershop.Shareable.DTO;
using Barbershop.Shareable.Response;
using MediatR;
using OperationResult;
using System.Text.Json.Serialization;

namespace Barbershop.Shareable.Request.Barber;

public record BarberWorksdayRequest(
    [property: JsonIgnore] string BarberId,
    List<WorkdayDTO> Workdays)
    : IRequest<Result<BarberWorksdayResponse>>;