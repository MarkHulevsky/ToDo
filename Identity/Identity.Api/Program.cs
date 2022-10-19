using Identity.Core.Configuration;
using Identity.Core.Grpc;
using Identity.Core.Grpc.Interfaces;
using Identity.Core.Validators;
using IdentityServer4.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserClient, GrpcUserClient>();

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

builder.Services.AddSingleton<ICorsPolicyService>(container =>
{
    var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();

    return new DefaultCorsPolicyService(logger)
    {
        AllowedOrigins = { webClientOrigin }
    };
});

builder.Services
    .AddIdentityServer()
    .AddResourceOwnerValidator<UserValidator>()
    .AddInMemoryClients(IdentityServerConfiguration.Clients)
    .AddInMemoryApiScopes(IdentityServerConfiguration.ApiScopes)
    .AddInMemoryApiResources(IdentityServerConfiguration.ApiResources)
    .AddInMemoryIdentityResources(IdentityServerConfiguration.IdentityResources)
    .AddDeveloperSigningCredential();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DefaultPolicy");

app.UseHttpsRedirection();

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();

app.Run();