using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Common
{
    public abstract class BaseRepository<T>(Context context) : IRepository<T> where T : BaseEntity
    {
        public virtual async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }
        public virtual async Task<ICollection<T>> GetByAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }
        public virtual async Task<T?> GetFirstByAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }

}