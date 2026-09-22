using Barbershop.Data;
using Barbershop.Data.Repository;
using Barbershop.Domain;
using Barbershop.Domain.Contract.Repository;
using Barbershop.Domain.Contract.Service;
using Barbershop.Domain.Entity;
using Barbershop.Domain.Service;
using Barbershop.Shareable;
using Barbershop.Shareable.Config;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace Barbershop.Ioc;

public static class ApplicationConfiguration
{
    public static void Configure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureAuthorization(configuration);
        services.ConfigureDbContext(configuration);
        services.ConfigureIdentityUser(configuration);
        services.ConfigureMediatR();
        services.ConfigureHttpOptions();
        services.ConfigurarExceptionHandler();
        services.ConfigurarFluentValidation();
        services.ConfigureServices();
        services.AddSingleton(configuration.Get<AppConfig>() ?? throw new ArgumentNullException("Configs is not defined"));
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
        services.AddScoped<IBarberRepository, BarberRepository>();
        services.AddScoped<IBarbershopRepository, BarbershopRepository>();
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

    // TODO: As validações não estão sendo realizadas.
    internal static void ConfigurarFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(ISharableEntryPoint).Assembly, includeInternalTypes: true);
    }

    private static void ConfigureIdentityUser(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<UserEntity, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = configuration.GetSection("Authentication:Identity:MinimumLengthPassword").Get<int>();
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.User.AllowedUserNameCharacters = configuration["Authentication:Identity:AllowedUserNameCharacters"]!;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
        })
            .AddEntityFrameworkStores<BarbershopDbContext>()
            .AddDefaultTokenProviders();
    }

    private static void ConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration.GetSection("Authentication:Key").Value!);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetSection("Authentication:Issuer").Value,
                    ValidateAudience = true,
                    ValidAudience = configuration.GetSection("Authentication:Scope").Value,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true
                };
            });

        services.AddAuthorization();
    }

    private static void ConfigureServices(this IServiceCollection services)
        => services.AddSingleton<ITokenService, TokenService>();

    // Add CORS
}