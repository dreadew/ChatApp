using ChatApp.Api.Hubs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseRouting();
app.MapHub<ChatHub>("/chat");

app.Run();
