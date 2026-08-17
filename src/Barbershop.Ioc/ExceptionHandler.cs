using Barbershop.Shareable.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Barbershop.Ioc;

public class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is BaseException ex)
        {
            logger.LogError("Ocorreu uma exceção mapeada: {TipoExcecao} --> {Mensagem}", ex.GetType().Name, exception.Message);

            var response = new
            {
                ex.Message,
                ex.Code
            };

            httpContext.Response.StatusCode = (int)ex.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }

        return false;
    }
}