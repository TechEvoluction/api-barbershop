using Barbershop.Api.Extensions;
using Barbershop.Shareable.Config;
using Barbershop.Shareable.Request;
using Barbershop.Shareable.Response;
using MediatR;

namespace Barbershop.Api.Endpoints;

internal static class BarbershopEndpoints
{
    public static void MapBarbershopEndpoint(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("barbershop")
            .WithTags("Barbershop");

        api.MapPost("operations", async (IMediator mediator, OperationsRequest req) => await mediator.SendCommand(req))
            .Produces<OperationsResponse>(StatusCodes.Status200OK)
            .WithSummary("Define os dias e horários de funcionamento da barbearia")
            .RequireAuthorization(policy => policy.RequireRole(Roles.Admin));
    }
}