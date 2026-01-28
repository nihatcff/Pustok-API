using Pustok.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.DataAccess.Repositories.Abstractions.Generic;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAll();
    Task<T> GetByIdAsync(Guid Id);

    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> SaveChangesAsync();
}
