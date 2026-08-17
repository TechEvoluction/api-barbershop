using Barbershop.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace Barbershop.Api.Endpoints;

internal static class EndpointExtensions
{
    public static async Task<IResult> SendCommand<T>(this IMediator mediator, IRequest<Result<T>> request, int statusCode = 200)
    {
        var result = await mediator.Send(request);
        return result.IsSuccess
            ? Results.Json(result.Value, statusCode: statusCode)
            : HandleError(result.Exception!);
    }

    public static async Task<IResult> SendCommand(this IMediator mediator, IRequest<Result> request, int statusCode = 200)
    {
        var result = await mediator.Send(request);
        return result.IsSuccess
            ? Results.StatusCode(statusCode)
            : HandleError(result.Exception!);
    }

    private static IResult HandleError(Exception error)
        => error switch
        {
            BaseException e => Results.Json(new { Mensagem = e.Message, e.Code }, statusCode: (int)e.StatusCode),
            _ => Results.StatusCode(500)
        };
}