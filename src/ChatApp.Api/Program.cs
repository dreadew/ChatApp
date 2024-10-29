using ChatApp.Api.Hubs;
using ChatApp.Infrastructure.DependencyInjection;
using ChatApp.Application.DependencyInjection;
using Serilog;
using ChatApp.Core.Interfaces.Auth;
using ChatApp.Infrastructure.Auth;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Добавление сервисов в контейнер
builder.Services.AddControllers();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseRouting();
app.MapHub<ChatHub>("/chat");

app.Run();
