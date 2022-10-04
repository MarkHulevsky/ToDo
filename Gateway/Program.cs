using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json");

builder.Services.AddOcelot();

var webClientOrigin = builder.Configuration.GetValue<string>("WebClientOrigin");

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy",
        policyBuilder => policyBuilder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins(webClientOrigin)
    );
});

var app = builder.Build();

app.UseCors("DefaultPolicy");

app.MapControllers();

await app.UseOcelot();

app.Run();