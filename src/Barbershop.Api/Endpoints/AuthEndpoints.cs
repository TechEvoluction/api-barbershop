using Barbershop.Api.Extensions;
using Barbershop.Shareable.Request.Auth;
using Barbershop.Shareable.Response;
using MediatR;
using System.Security.Claims;

namespace Barbershop.Api.Endpoints;

internal static class AuthEndpoints
{
    public static void MapAuthEndpoint(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("auth")
            .WithTags("Authentication");

        api.MapPost("register", async (IMediator mediator, RegisterUserRequest req) => await mediator.SendCommand(req, 201))
            .Produces(StatusCodes.Status201Created)
            .WithSummary("Realiza a criação de um usuário")
            .AllowAnonymous();

        api.MapPost("login", async (IMediator mediator, LoginRequest req) => await mediator.SendCommand(req))
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .WithSummary("Realiza o login de um usuário")
            .AllowAnonymous();

        api.MapPost("refresh-token", async (IMediator mediator, RefreshTokenRequest req) => await mediator.SendCommand(req))
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .WithSummary("Gera novo token com base no refresh token")
            .AllowAnonymous();

        api.MapPost("forget-password", async (IMediator mediator, ForgetPasswordRequest req) => await mediator.SendCommand(req))
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Gera token para redefinição de senha")
            .AllowAnonymous();

        api.MapPost("reset-password", async (IMediator mediator, ResetPasswordRequest req) => await mediator.SendCommand(req))
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Aplicar nova senha")
            .AllowAnonymous();

        api.MapPost("change-password", async (IMediator mediator, ClaimsPrincipal user, ChangePasswordRequest req) => await mediator.SendCommand(req with { UserId = user.GetUserId() }))
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Permite que o usuário logado altere a senha")
            .RequireAuthorization();

        api.MapPost("confirm/email", async (IMediator mediator, ConfirmEmailRequest req) => await mediator.SendCommand(req))
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Confirmar email do usuário")
            .AllowAnonymous();

        api.MapPut("profile", async (IMediator mediator, UpdateProfileRequest req) => await mediator.SendCommand(req))
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Atualiza os dados do perfil do usuário")
            .RequireAuthorization();
    }
}