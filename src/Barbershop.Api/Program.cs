using Barbershop.Api.Configuration;
using Barbershop.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerConfiguration();

builder.Services.Configure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseSwaggerConfiguration();

app.UseHttpsRedirection();

app.AddEndpoints();

app.Run();
