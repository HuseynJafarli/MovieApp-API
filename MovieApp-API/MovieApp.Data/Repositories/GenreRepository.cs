using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using MovieApp.Data.Contexts;

namespace MovieApp.Data.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
