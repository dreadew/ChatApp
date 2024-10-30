namespace ChatApp.Core.Interfaces.Migrations;

public interface IDatabaseMigrator
{
  Task MigrateAsync();
}