using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using MovieApp.Data.Contexts;
using System.Linq.Expressions;

namespace MovieApp.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly AppDbContext dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public DbSet<TEntity> Table => dbContext.Set<TEntity>();

        public async Task<int> CommitAsync()
            => await dbContext.SaveChangesAsync();

        public async Task CreateAsync(TEntity entity)
            => await Table.AddAsync(entity);

        public async void Delete(TEntity entity)
            => Table.Remove(entity);

        public IQueryable<TEntity> GetByExpression(bool asNoTracking = false, Expression<Func<TEntity, bool>>? expression = null, params string[] includes)
        {
            var query = Table.AsQueryable();

            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            query = asNoTracking == true
                ? query.AsNoTracking()
                : query;

            return expression is not null
                ? query.Where(expression)
                : query;

        }

        public async Task<TEntity> GetByIdAsync(int id)
            => await Table.FindAsync(id);
    }
}
