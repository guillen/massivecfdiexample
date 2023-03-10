using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Domain.Services
{
    public interface IService<T>
    {
        Task<ICollection<T>> AllAsync();
        Task<ICollection<T>> AllQueryAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> Query(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
    }
}
