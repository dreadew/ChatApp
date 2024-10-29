using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ChatApp.Infrastructure;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
  const string PostgresConnectionString = "PostgresConnection";
  public ApplicationDbContext CreateDbContext(string[] args)
  {
    var config = new ConfigurationBuilder()
      .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ChatApp.Api"))
      .AddJsonFile("appsettings.json")
      .Build();

    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    var connectionString = config.GetConnectionString(PostgresConnectionString);
    optionsBuilder.UseNpgsql(connectionString);

    return new ApplicationDbContext(optionsBuilder.Options);
  }
}