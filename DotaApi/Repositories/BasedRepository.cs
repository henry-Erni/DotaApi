using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DotaApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DotaApi.Repositories
{
    public class BasedRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BasedRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<T> Get(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            var entity = await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with Id {id} not found.");
            }
            return entity;
        }

        public async Task<T> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(Guid id)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with Id {id} not found.");
            }
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}