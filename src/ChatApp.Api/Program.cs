using ChatApp.Api.Hubs;
using ChatApp.Infrastructure.DependencyInjection;
using ChatApp.Application.DependencyInjection;
using Serilog;
using ChatApp.Core.Interfaces.Auth;
using ChatApp.Infrastructure.Auth;
using ChatApp.Api.Filters;

const string FrontendOriginEnv = "FrontendOrigin";
const string RedisConnectionEnv = "Redis";

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var frontendOrigin = configuration[FrontendOriginEnv];

if (frontendOrigin == null)
{
	throw new InvalidOperationException($"{FrontendOriginEnv} is not specified");
}

builder.Host.UseSerilog((context, configuration) =>
{
	configuration.ReadFrom.Configuration(context.Configuration);
	 if (context.HostingEnvironment.IsDevelopment())
    {
        configuration.MinimumLevel.Debug(); // Устанавливаем уровень Debug для режима разработки
    }
});

// Добавление сервисов в контейнер
builder.Services.AddScoped<JwtAuthFilter>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddControllers();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
	var connection = configuration.GetConnectionString(RedisConnectionEnv);
	options.Configuration = connection;
});

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.WithOrigins(frontendOrigin)
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials();
	});
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
//app.UseRouting();
app.MapHub<ChatHub>("/chat");
app.UseAuthorization();
app.MapControllers();

app.Run();
