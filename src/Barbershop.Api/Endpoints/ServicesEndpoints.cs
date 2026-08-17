using Barbershop.Shareable.Request;
using Barbershop.Shareable.Response;
using MediatR;

namespace Barbershop.Api.Endpoints;

internal static class ServicesEndpoints
{
    public static void MapServiceEndpoint(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("services")
            .WithTags("Services");

        api.MapPost(string.Empty, async (IMediator mediator, CreateServiceRequest req) => await mediator.SendCommand(req, 201))
            .Produces(StatusCodes.Status201Created)
            .WithSummary("Realiza a criação de um serviço");

        api.MapPut(string.Empty, async (IMediator mediator, UpdateServiceRequest req) => await mediator.SendCommand(req))
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Realiza a atualização de um serviço");

        api.MapPost("{id:guid}/activate", async (IMediator mediator, Guid Id) => await mediator.SendCommand(new ActivateServiceRequest(Id)))
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Realiza a ativação de um serviços");

        api.MapPost("{id:guid}/deactivate", async (IMediator mediator, Guid Id) => await mediator.SendCommand(new DeactivateServiceRequest(Id)))
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Realiza a desativação de um serviços");

        api.MapGet("{id:guid}", async (IMediator mediator, Guid id) => await mediator.SendCommand(new GetServiceRequest(id)))
            .Produces<ServiceResponse>(StatusCodes.Status200OK)
            .WithSummary("Obtém um serviço pelo id");

        api.MapGet(string.Empty, async (IMediator mediator) => await mediator.SendCommand(new GetServicesRequest()))
            .Produces<ServiceResponse[]>(StatusCodes.Status200OK)
            .WithSummary("Obtém todos os serviços");

        api.MapPost("{id:guid}/promotion", async (IMediator mediator, Guid Id, ServiceOnSaleRequest req) => await mediator.SendCommand(req with { Id = Id }))
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Registra um serviço em promoção");
    }
}