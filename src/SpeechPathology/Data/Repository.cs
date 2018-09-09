using SQLiteNetExtensionsAsync.Extensions;
using SpeechPathology.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpeechPathology.Data
{
    public class Repository<T> : IRepository<T> where T : ModelBase, new()
    {
        private SQLiteAsyncConnection db;

        public Repository(string databaseFile)
        {
            this.db = new SQLiteAsyncConnection(databaseFile);
        }

        public AsyncTableQuery<T> AsQueryable() =>
            db.Table<T>();
        
        public async Task<List<T>> GetAllWithChildren() =>
           await db.GetAllWithChildrenAsync<T>();

        public async Task DeleteAllAsync(IEnumerable<T> objects, bool recursive = false) =>
            await db.DeleteAllAsync(objects, recursive);

        public async Task InsertAllAsync(IEnumerable<T> objects) => 
            await db.InsertAllAsync(objects);

        public async Task<List<T>> GetAll() =>
            await db.Table<T>().ToListAsync();

        public async Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            var query = db.Table<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy<TValue>(orderBy);

            return await query.ToListAsync();
        }

        public async Task<T> Get(Int64 id) =>
             await db.FindAsync<T>(id);

        public async Task<T> Get(Expression<Func<T, bool>> predicate) =>
            await db.FindAsync<T>(predicate);

        public async Task<int> Insert(T entity) =>
             await db.InsertAsync(entity);

        public async Task<int> Update(T entity) =>
             await db.UpdateAsync(entity);

        public async Task<int> Delete(T entity) =>
             await db.DeleteAsync(entity);
    }
}