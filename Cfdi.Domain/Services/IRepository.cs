using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Domain.Services
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(int id);
        Task<ICollection<T>> GetAsync();
        Task<T> QueryAsync(Expression<Func<T, bool>> match);
        Task<ICollection<T>> QueryByAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> QueryBy(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
    }
}
