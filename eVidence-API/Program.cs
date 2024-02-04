using Microsoft.AspNetCore.Mvc;
using eVidence_API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "corsPolicy",
        policy =>
        {
            policy.WithOrigins(eVidence_API.Environment.FRONT_ADDRESS).AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("corsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run(eVidence_API.Environment.IP_ADDRESS);
