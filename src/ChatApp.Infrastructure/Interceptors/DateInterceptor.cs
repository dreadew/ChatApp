using ChatApp.Core.Interfaces.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Interceptors;

public class DateInterceptor : SaveChangesInterceptor
{
  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;
            if(dbContext == null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var entries = dbContext.ChangeTracker.Entries<IAuditableEntity>()
                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added)
                .ToList();

            foreach(var entry in entries)
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Property(x=>x.CreatedAt).CurrentValue = DateTime.UtcNow;
                }
                else
                {
                    entry.Property(x=>x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

}