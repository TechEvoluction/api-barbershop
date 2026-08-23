using Barbershop.Api.Configuration;
using Barbershop.Data;
using Barbershop.Ioc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerConfiguration();

builder.Services.Configure(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        await DbInitializer.SeedRolesAsync(services);
    }
}

app.UseHttpsRedirection();

app.AddEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
