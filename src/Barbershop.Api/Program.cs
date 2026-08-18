using Barbershop.Api.Configuration;
using Barbershop.Ioc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerConfiguration();

builder.Services.Configure(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseSwaggerConfiguration();

app.UseHttpsRedirection();

app.AddEndpoints();

app.Run();
