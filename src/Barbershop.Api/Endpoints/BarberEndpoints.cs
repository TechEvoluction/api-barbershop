using Barbershop.Api.Extensions;
using Barbershop.Shareable.Config;
using Barbershop.Shareable.Request.Barber;
using Barbershop.Shareable.Response;
using MediatR;
using System.Security.Claims;

namespace Barbershop.Api.Endpoints;

internal static class BarberEndpoints
{
    public static void MapBarberEndpoint(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("barbers")
            .WithTags("Barbers");

        api.MapPost("invitation", async (IMediator mediator, BarberInvitationRequest req) => await mediator.SendCommand(req, 202))
            .Produces<CreateBarberResponse>(StatusCodes.Status202Accepted)
            .WithSummary("Realiza o pré cadastro de um barbeiro e envia um convite para aceite")
            .RequireAuthorization(policy => policy.RequireRole(Roles.Admin));

        api.MapPost("{barberId:guid}/accept-invitation", async (IMediator mediator, Guid barberId, AcceptBarberInvitationRequest req) => await mediator.SendCommand(req with { BarberId = barberId }))
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Finaliza cadastro do barbeiro com base no convite")
            .AllowAnonymous();

        api.MapPost("{barberId}/worksday", async (IMediator mediator, string barberId, BarberWorksdayRequest req) => await mediator.SendCommand(req with { BarberId = barberId }))
            .Produces<BarberWorksdayResponse>(StatusCodes.Status200OK)
            .WithSummary("Gerencia os dias de trabalho do barbeiro")
            .RequireAuthorization(policy => policy.RequireRole(Roles.Admin));

        api.MapPost("block-schedule", async (IMediator mediator, ClaimsPrincipal user, BlockScheduleRequest req) => await mediator.SendCommand(req with { BarberId = user.GetUserId() }))
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Bloqueia a agenda do barbeiro")
            .RequireAuthorization(policy => policy.RequireRole(Roles.Barber));

        // recusar/reagendar agendamento
        // filtrar agendamentos
    }
}