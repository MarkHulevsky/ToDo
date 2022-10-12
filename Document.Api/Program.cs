using Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Pdf.BusinessLogic;
using Pdf.BusinessLogic.Constants;
using Pdf.BusinessLogic.HttpClients;
using Pdf.BusinessLogic.HttpClients.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfigurationSection identitySettingsSection = builder.Configuration.GetSection("IdentitySettings");
builder.Services.Configure<IdentitySettingsModel>(identitySettingsSection);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = identitySettingsSection.GetValue<string>("IssuerUrl");
        options.Audience = identitySettingsSection.GetValue<string>("Audience");
    });

builder.Services.AddHttpContextAccessor();

var toDoServiceOrigin = builder.Configuration.GetValue<string>("ToDoApiServiceOrigin");

builder.Services.AddHttpClient(HttpClientsConstants.IDENTITY_HTTP_CLIENT_NAME, client =>
    client.BaseAddress = new Uri(identitySettingsSection.GetValue<string>("IssuerUrl"))
);

builder.Services.AddHttpClient<IToDoHttpClient, ToDoHttpClient>(client =>
    client.BaseAddress = new Uri(toDoServiceOrigin)
);

builder.Services.ConfigureBusinessLogic()
    .ConfigureAutoMapper();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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