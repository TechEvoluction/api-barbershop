using MediatR;
using OperationResult;

namespace Barbershop.Shareable.Request;

public record SchedulesAvailableTimesRequest() : IRequest<Result>;