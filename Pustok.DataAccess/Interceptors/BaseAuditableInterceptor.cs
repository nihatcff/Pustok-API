using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Pustok.Core.Entities.Common;
using Pustok.DataAccess.Contexts;

namespace Pustok.DataAccess.Interceptors;
internal class BaseAuditableInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateAUditableDatas(eventData);

        return base.SavingChanges(eventData, result);
    }


    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateAUditableDatas(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    private static void UpdateAUditableDatas(DbContextEventData eventData)
    {
        if (eventData.Context is AppDbContext appDbContext)
        {
            var entities = appDbContext.ChangeTracker.Entries<BaseAuditableEntity>().ToList();
            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.Entity.CreatedDate = DateTime.UtcNow;
                        entity.Entity.CreatedBy = "System";
                        break;
                    case EntityState.Modified:
                        entity.Entity.UpdatedDate = DateTime.UtcNow;
                        entity.Entity.UpdatedBy = "System";
                        break;
                    case EntityState.Deleted:
                        entity.State = EntityState.Modified;
                        entity.Entity.IsDeleted = true;
                        entity.Entity.DeletedDate = DateTime.UtcNow;
                        entity.Entity.DeletedBy = "System";
                        break;
                }
            }
        }
    }
}
