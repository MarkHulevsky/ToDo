using Identity.Core.Configuration;
using Identity.Core.Grpc;
using Identity.Core.Grpc.Interfaces;
using Identity.Core.Validators;

/*------------------------ConfigureServices-------------------------*/

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserClient, GrpcUserClient>();

builder.Services
    .AddIdentityServer()
    .AddResourceOwnerValidator<UserValidator>()
    .AddInMemoryClients(IdentityServerConfiguration.Clients)
    .AddInMemoryApiScopes(IdentityServerConfiguration.ApiScopes)
    .AddInMemoryApiResources(IdentityServerConfiguration.ApiResources)
    .AddInMemoryIdentityResources(IdentityServerConfiguration.IdentityResources)
    .AddDeveloperSigningCredential();

/*------------------------------------------------------------*/




/*------------------------Configure-------------------------*/

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();

app.Run();

/*-------------------------------------------------------------*/