using ChatApp.Application.Interfaces.Services;
using ChatApp.Application.Mappings;
using ChatApp.Core.Interfaces.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Application.DependencyInjection;

public static class DependencyInjection
{
  public static void AddApplication(this IServiceCollection services)
  {
    services.AddAutoMapper(typeof(MappingProfile));
    InitServices(services);
  }

  public static void InitServices(this IServiceCollection services)
  {
    services.AddScoped<IUserService, Services.UserService>();
    services.AddScoped<IChatService, Services.ChatService>();
  }
}