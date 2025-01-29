using System.Linq.Expressions;
using DotaApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DotaApi.Repositories
{
    public class BasedRepository<T> : IBaseRepository<T> where T : class
    {   
        private readonly AppDbContext _context;

        public BasedRepository(AppDbContext context)
        {
            _context = context;
        }
     
        public Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return query.ToListAsync();
        }

        public async Task<T> Get(Guid Id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == Id) ?? throw new KeyNotFoundException($"Entity with Id {Id} not found.");
        }

        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return Task.FromResult(entity);
        }

        public async Task<T> Delete(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id) ?? throw new KeyNotFoundException($"Entity with Id {id} not found.");
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }


    }
}
