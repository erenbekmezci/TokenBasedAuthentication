using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.UnitOfWorks
{
    public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
    {
        public Task<int> SaveChangesAsync()
        {
            return appDbContext.SaveChangesAsync();
        }
    }
}
