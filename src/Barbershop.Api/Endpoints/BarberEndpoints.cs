using Barbershop.Api.Extensions;
using Barbershop.Shareable.Config;
using Barbershop.Shareable.Request.Barber;
using Barbershop.Shareable.Response;
using MediatR;

namespace Barbershop.Api.Endpoints;

internal static class BarberEndpoints
{
    public static void MapBarberEndpoint(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("barbers")
            .WithTags("Barbers");

        api.MapPost("invitation", async (IMediator mediator, BarberInvitationRequest req) => await mediator.SendCommand(req, 202))
            .Produces<CreateBarberResponse>(StatusCodes.Status202Accepted)
            .WithSummary("Realiza o pré cadastro de um barbeiro e envia um convite para aceite");
            //.RequireAuthorization(policy => policy.RequireRole(Roles.Admin));

        api.MapPost("{barberId:guid}/accept-invitation", async (IMediator mediator, Guid barberId, AcceptBarberInvitationRequest req) => await mediator.SendCommand(req with { BarberId = barberId }))
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Finaliza cadastro do barbeiro com base no convite")
            .AllowAnonymous();

        api.MapPost("{barberId:guid}/worksday", async (IMediator mediator, Guid barberId, BarberInvitationRequest req) => await mediator.SendCommand(req))
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Gerencia os dias de trabalho do barbeiro");
            //.RequireAuthorization(policy => policy.RequireRole(Roles.Admin));

        // bloquear agenda
        // recusar/reagendar agendamento
    }
}