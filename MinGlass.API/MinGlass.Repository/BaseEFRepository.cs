using Microsoft.EntityFrameworkCore;
using MinGlass.Models;
using MinGlass.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinGlass.Repository
{
    public class BaseEFRepository<T> where T : BaseEntity
    {
        private readonly ClientAppContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseEFRepository(ClientAppContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        protected IQueryable<T> GetQuery()
        {
            return _dbSet.AsQueryable();
        }

        protected virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        protected virtual void AddRange(List<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        protected virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        protected virtual void UpdateRange(List<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        protected virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        protected virtual void RemoveRange(List<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        protected async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
