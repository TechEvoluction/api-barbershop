using Barbershop.Api.Endpoints;

namespace Barbershop.Api.Configuration;

internal static class EndpointsConfiguration
{
    public static void AddEndpoints(this IEndpointRouteBuilder app)
    {
        var apiGroup = app.MapGroup("/api");

        apiGroup.MapServiceEndpoint();
        apiGroup.MapAuthEndpoint();
        apiGroup.MapBarberEndpoint();
    }
}