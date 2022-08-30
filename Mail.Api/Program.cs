using Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfigurationSection identitySettingsSection = builder.Configuration.GetSection("IdentitySettings");
builder.Services.Configure<IdentitySettingsModel>(identitySettingsSection);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.ApiName = identitySettingsSection.GetValue<string>("Audience");
        options.Authority = identitySettingsSection.GetValue<string>("IssuerUrl");
        options.ApiSecret = identitySettingsSection.GetValue<string>("ApiSecret");
    });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();