using ChatApp.Core.Interfaces.Repositories;
using ChatApp.Infrastructure.Interceptors;
using ChatService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
  const string PostgresConnectionString = "PostgresConnection";
  public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString(PostgresConnectionString);
    services.AddSingleton<DateInterceptor>();
    services.AddDbContext<ApplicationDbContext>(options => 
    {
      options.UseNpgsql(connectionString);
    });
    services.InitRepositories();
  }

  public static void InitRepositories(this IServiceCollection services)
  {
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IChatRepository, ChatRepository>();
    services.AddScoped<IMessageRepository, MessageRepository>();
  }
}