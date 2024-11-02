using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task AddAsync(T entity);
    Task<ICollection<T>> GetByAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetFirstByAsync(Expression<Func<T, bool>> predicate);
    Task SaveChangesAsync();
}