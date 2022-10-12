using Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Users.BusinessLogic.Services;
using Users.BusinessLogic.Services.Grpc;
using Users.BusinessLogic.Services.Interfaces;
using Users.DataAccess;
using Users.DataAccess.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfigurationSection identitySettingsSection = builder.Configuration.GetSection("IdentitySettings");
builder.Services.Configure<IdentitySettingsModel>(identitySettingsSection);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = identitySettingsSection.GetValue<string>("IssuerUrl");
        options.ApiName = identitySettingsSection.GetValue<string>("Audience");
    });

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddGrpc();

var app = builder.Build();

await DbInitializer.InitializeDatabaseAsync(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapGrpcService<GrpcAccountService>();
});

app.UseAuthentication();

app.Run();