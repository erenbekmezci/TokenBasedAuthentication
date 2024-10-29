using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> dbSet = context.Set<T>();
        protected AppDbContext _context = context;

        public async ValueTask AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async ValueTask<T?> GetByIdAsync(int id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity != null)
               _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            //dbSet.Update(entity);
        }

        public  IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return  dbSet.Where(predicate).AsNoTracking();
        }
    }
}
