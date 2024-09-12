using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieApp.Core.Entities;
using System.Linq.Expressions;

namespace MovieApp.Core.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        public DbSet<TEntity> Table { get; }
        Task CreateAsync(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> GetByExpression(bool asNoTracking = false, Expression<Func<TEntity, bool>>? expression = null, params string[] includes);
        Task<TEntity> GetByIdAsync(int id);
        Task<int> CommitAsync();
    }
}
