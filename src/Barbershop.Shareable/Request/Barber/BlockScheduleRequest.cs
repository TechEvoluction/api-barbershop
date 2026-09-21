using MediatR;
using OperationResult;
using System.Text.Json.Serialization;

namespace Barbershop.Shareable.Request.Barber;

public record BlockScheduleRequest(
    [property: JsonIgnore] string BarberId,
    DateOnly Date,
    TimeOnly? StartTime,
    TimeOnly? EndTime,
    string Reason)
    : IRequest<Result>;