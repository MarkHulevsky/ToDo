using System.Reflection;
using AutoMapper;
using Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using ToDo.BusinessLogic;
using ToDo.BusinessLogic.HttpClients;
using ToDo.BusinessLogic.HttpClients.Interfaces;
using ToDo.BusinessLogic.Services;
using ToDo.BusinessLogic.Services.Interfaces;
using ToDo.DataAccess;
using ToDo.DataAccess.Repositories;
using ToDo.DataAccess.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfigurationSection identitySettingsSection = builder.Configuration.GetSection("IdentitySettings");
builder.Services.Configure<IdentitySettingsModel>(identitySettingsSection);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = identitySettingsSection.GetValue<string>("IssuerUrl");
        options.ApiName = identitySettingsSection.GetValue<string>("Audience");
        options.ApiSecret = identitySettingsSection.GetValue<string>("ApiSecret");
    });

builder.Services.AddHttpClient<IMailHttpClient, MailHttpClient>(httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration.GetValue<string>("MailServiceUrl"));
});

builder.Services.AddHttpContextAccessor();

builder.Services
    .ConfigureDataAccessServices()
    .ConfigureBusinessLogicServices()
    .ConfigureAutoMapper();

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