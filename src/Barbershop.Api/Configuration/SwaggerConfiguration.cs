using System.Diagnostics.CodeAnalysis;

namespace Barbershop.Api.Configuration;

[ExcludeFromCodeCoverage]
internal static class SwaggerConfiguration
{
    private const string API_TITLE = "Barbershop.Api";

    public static void AddSwaggerConfiguration(this IServiceCollection services)
        => services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info.Title = API_TITLE;
                document.Info.Version = "v1";
                document.Info.Description = "API de gerenciamento da barbearia";
                return Task.CompletedTask;
            });
        });

    public static void UseSwaggerConfiguration(this WebApplication app)
    {
        app.MapOpenApi();
        app.UseSwaggerUI(opt =>
        {
            opt.SwaggerEndpoint("/openapi/v1.json", $"{API_TITLE} v1");
        });
    }
}