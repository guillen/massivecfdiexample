using Cfdi.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Domain.Common
{
    public class Service<T> : IService<T>
    {
        protected readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async virtual Task<ICollection<T>> AllAsync()
        {
            return await _repository.GetAsync();
        }

        public async virtual Task<ICollection<T>> AllQueryAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.QueryByAsync(predicate);
        }

        public async virtual Task<T> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.QueryAsync(predicate);
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _repository.QueryBy(predicate);
        }

        public async virtual Task<T> AddAsync(T entity)
        {
            return await _repository.AddAsync(entity);
        }
    }
}
