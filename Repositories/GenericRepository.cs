using System;
using System.Linq.Expressions;
using ExpenseApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApi.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ExpenseContext _context;

        public GenericRepository(ExpenseContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRange(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async virtual Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async virtual Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<int> Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EntityExists(entity))
                {
                    return 0;
                }
                else
                {
                    throw;
                }
            }
        }

        private bool EntityExists(T entity)
        {
            var keyNames = _context
                            .Model
                            .FindEntityType(typeof(T))
                            .FindPrimaryKey()
                            .Properties;
            object[] primaryKeys = new object[keyNames.Count];

            for (int i = 0; i < keyNames.Count; i++)
            {
                primaryKeys[i] = entity.GetType().GetProperty(keyNames[i].ToString()).GetValue(entity);
            }
            return _context.Set<T>().FindAsync(primaryKeys) != null;
        }
    }
}
