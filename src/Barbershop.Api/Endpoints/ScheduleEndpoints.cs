using Barbershop.Api.Extensions;
using Barbershop.Shareable.Request;
using MediatR;

namespace Barbershop.Api.Endpoints;

internal static class ScheduleEndpoints
{
    public static void MapScheduleEndpoint(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("schedules")
            .WithTags("Schedules");

        api.MapGet("available-times", async (IMediator mediator) => await mediator.SendCommand(new SchedulesAvailableTimesRequest()))
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Retorna os horários disponíveis de cada barbeiro para agendamento")
            .RequireAuthorization();
    }
}