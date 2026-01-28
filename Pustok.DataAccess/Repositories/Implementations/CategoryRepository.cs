using Pustok.DataAccess.Contexts;
using Pustok.DataAccess.Repositories.Abstractions;
using Pustok.DataAccess.Repositories.Implementations.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.DataAccess.Repositories.Implementations
{
    internal class CategoryRepository(AppDbContext _context) : Repository<Category>(_context ), ICategoryRepository
    {
    }
}
