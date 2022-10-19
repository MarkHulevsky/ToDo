using Common.Models;
using Mail.BusinessLogic;
using Mail.BusinessLogic.BackgroundWorkers;
using Mail.BusinessLogic.Constants;
using Mail.BusinessLogic.HttpClients;
using Mail.BusinessLogic.HttpClients.Interfaces;
using Mail.BusinessLogic.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfigurationSection identitySettingsSection = builder.Configuration.GetSection("IdentitySettings");
builder.Services.Configure<IdentitySettingsModel>(identitySettingsSection);

IConfigurationSection rabbitMqSettingsSection = builder.Configuration.GetSection("RabbitMQ");
builder.Services.Configure<RabbitMqSettingsModel>(rabbitMqSettingsSection);

IConfigurationSection smtpSettingsSection = builder.Configuration.GetSection("SmtpSettings");
builder.Services.Configure<SmtpSettingsModel>(smtpSettingsSection);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.ApiName = identitySettingsSection.GetValue<string>("Audience");
        options.Authority = identitySettingsSection.GetValue<string>("IssuerUrl");
    });

builder.Services.ConfigureBusinessLogic();

builder.Services.AddHostedService<MessageQueueBackgroundService>();

builder.Services.AddHttpClient(HttpClientsConstants.IDENTITY_HTTP_CLIENT_NAME, client =>
    client.BaseAddress = new Uri(identitySettingsSection.GetValue<string>("IssuerUrl"))
);

var userApiOrigin = builder.Configuration.GetValue<string>("UserApiOrigin");

builder.Services.AddHttpClient<IUserHttpClient, UserHttpClient>(client =>
    client.BaseAddress = new Uri(userApiOrigin)
);

var documentApiOrigin = builder.Configuration.GetValue<string>("DocumentApiOrigin");

builder.Services.AddHttpClient<IDocumentHttpClient, DocumentHttpClient>(client =>
    client.BaseAddress = new Uri(documentApiOrigin)
);

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