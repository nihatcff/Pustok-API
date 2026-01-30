using Microsoft.EntityFrameworkCore;
using Pustok.Core.Entities;
using Pustok.Core.Entities.Common;
using Pustok.DataAccess.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.DataAccess.Contexts;

internal class AppDbContext(BaseAuditableInterceptor _interceptor, DbContextOptions options) : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
        base.OnModelCreating(modelBuilder);
    }
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_interceptor);
        base.OnConfiguring(optionsBuilder);
    }

/*
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entities = this.ChangeTracker.Entries<BaseAuditableEntity>().ToList();
        foreach (var entity in entities)
        {
            switch(entity.State)
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
        return base.SaveChangesAsync(cancellationToken);
    }
*/
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}
