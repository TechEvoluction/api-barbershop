using Barbershop.Shareable.Request;
using MediatR;
using OperationResult;

namespace Barbershop.Domain.Handler;

public class SchedulesHandler
    : IRequestHandler<SchedulesAvailableTimesRequest, Result>
{
    public Task<Result> Handle(SchedulesAvailableTimesRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}