using ChatApp.Core.Interfaces.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChatApp.Infrastructure;

public class DatabaseMigrator : IDatabaseMigrator
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DatabaseMigrator> _logger;

    public DatabaseMigrator(ApplicationDbContext dbContext, ILogger<DatabaseMigrator> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task MigrateAsync()
    {
        await _dbContext.Database.MigrateAsync();
        _logger.LogInformation("Migration completed successfully");
    }
}