using Microsoft.EntityFrameworkCore;
using Pustok.Core.Entities.Common;
using Pustok.DataAccess.Contexts;
using Pustok.DataAccess.Repositories.Abstractions.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.DataAccess.Repositories.Implementations.Generic;

internal class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity) ;
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        var result = await _context.Set<T>().AnyAsync(expression);
        return result;
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public IQueryable<T> GetAll(bool ignoreQueryFilter = false)
    {
        var query = _context.Set<T>().AsQueryable();

        if(ignoreQueryFilter)
            query = query.IgnoreQueryFilters();

        return query;
    }

    public Task<T?> GetAsync(Expression<Func<T, bool>> expression)
    {
        var entity = _context.Set<T>().FirstOrDefaultAsync(expression);
        return entity;
    }


    public async Task<T?> GetByIdAsync(Guid Id)
    {
        var result = await _context.Set<T>().FindAsync(Id);
        return result; 
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity); 
    }
}
