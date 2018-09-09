using SpeechPathology.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpeechPathology.Data
{
    public interface IRepository<T> where T : ModelBase, new()
    {
        Task<List<T>> GetAll();
        Task<T> Get(Int64 id);
        Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        AsyncTableQuery<T> AsQueryable();
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);

        Task<List<T>> GetAllWithChildren();
        Task DeleteAllAsync(IEnumerable<T> objects, bool recursive = false);
        Task InsertAllAsync(IEnumerable<T> objects);
    }
}
