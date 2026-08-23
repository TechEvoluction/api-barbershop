using Microsoft.OpenApi;
using System.Diagnostics.CodeAnalysis;

namespace Barbershop.Api.Configuration;

[ExcludeFromCodeCoverage]
internal static class SwaggerConfiguration
{
    private const string API_TITLE = "Barbershop.Api";
    private const string SECURITY_SCHEME_NAME = "Bearer";

    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        => services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info.Title = API_TITLE;
                document.Info.Version = "v1";
                document.Info.Description = "API de gerenciamento da barbearia";


                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
                document.Components?.SecuritySchemes?[SECURITY_SCHEME_NAME] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insert JWT token here"
                };

                document.Security =
                [
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecuritySchemeReference(SECURITY_SCHEME_NAME)
                        ] = []
                    }
                ];

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