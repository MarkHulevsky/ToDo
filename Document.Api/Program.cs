using Azure.Storage.Blobs;
using Common.Models;
using Document.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Document.BusinessLogic;
using Document.BusinessLogic.Constants;
using Document.BusinessLogic.HttpClients;
using Document.BusinessLogic.HttpClients.Interfaces;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddSingleton(_ =>
    new BlobServiceClient(builder.Configuration.GetValue<string>("AzureBlobStorageConnectionString"))
);

builder.Services
    .ConfigureDataAccess()
    .ConfigureBusinessLogic()
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