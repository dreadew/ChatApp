using ChatApp.Api.Hubs;
using ChatApp.Infrastructure.DependencyInjection;
using ChatApp.Application.DependencyInjection;
using Serilog;
using ChatApp.Core.Interfaces.Auth;
using ChatApp.Infrastructure.Auth;
using ChatApp.Api.Filters;
using ChatApp.Core.Interfaces.Migrations;

const string FrontendOriginEnv = "FrontendOrigin";
const string RedisConnectionEnv = "Redis";

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var frontendOrigin = configuration[FrontendOriginEnv];

if (frontendOrigin == null)
{
	throw new InvalidOperationException($"{FrontendOriginEnv} is not specified");
}

/*builder.Host.UseSerilog((context, configuration) =>
{
	configuration
		.ReadFrom
		.Configuration(context.Configuration)
    .WriteTo.Console();
});*/

builder.Services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

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

using (var scope = app.Services.CreateScope())
{
	var migrator = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();
	await migrator.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<ChatHub>("/chat");
app.UseAuthorization();
app.MapControllers();

Log.Information("Server is running at {Url}", builder.WebHost.GetSetting("urls"));

app.Run();
