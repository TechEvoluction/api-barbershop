using Barbershop.Data;
using Barbershop.Data.Repository;
using Barbershop.Domain;
using Barbershop.Domain.Contract.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Barbershop.Ioc;

public static class ApplicationConfiguration
{
    public static void Configure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        services.ConfigureMediatR();
        services.ConfigurarExceptionHandler();
        services.ConfigureHttpOptions();
    }

    private static void ConfigureMediatR(this IServiceCollection services)
        => services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IDomainEntryPoint).Assembly));

    private static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<BarbershopDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
    }

    private static void ConfigurarExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<ExceptionHandler>();
        services.AddProblemDetails();
    }

    private static void ConfigureHttpOptions(this IServiceCollection services)
        => services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

    // Add CORS
}